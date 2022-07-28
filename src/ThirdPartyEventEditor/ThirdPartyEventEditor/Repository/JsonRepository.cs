namespace ThirdPartyEventEditor.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Web;
    using Newtonsoft.Json;
    using ThirdPartyEventEditor.Models;

    public class JsonRepository : IDisposable
    {
        private readonly ReaderWriterLockSlim _readWriteLockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public List<ThirdPartyEvent> Insert(
            List<ThirdPartyEvent> thirdPartyEvents,
            ThirdPartyEvent thirdPartyEvent,
            HttpPostedFileBase eventLogoImageData,
            ThirdPartyEvent updatedthirdPartyEvent = null)
        {
            thirdPartyEvent.EventLogoImage = ConvertImageToBase64(eventLogoImageData);
            thirdPartyEvents.Add(thirdPartyEvent);
            return thirdPartyEvents;
        }

        public List<ThirdPartyEvent> Update(
            List<ThirdPartyEvent> thirdPartyEvents,
            ThirdPartyEvent thirdPartyEvent,
            HttpPostedFileBase eventLogoImageData,
            ThirdPartyEvent updatedthirdPartyEvent)
        {
            updatedthirdPartyEvent.EventLogoImage = ConvertImageToBase64(eventLogoImageData);
            thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == thirdPartyEvent.Description && x.Name == thirdPartyEvent.Name));
            thirdPartyEvents.Add(updatedthirdPartyEvent);
            return thirdPartyEvents;
        }

        public List<ThirdPartyEvent> Delete(
            List<ThirdPartyEvent> thirdPartyEvents,
            ThirdPartyEvent thirdPartyEvent,
            HttpPostedFileBase eventLogoImageData = null,
            ThirdPartyEvent updatedthirdPartyEvent = null)
        {
            thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == thirdPartyEvent.Description && x.Name == thirdPartyEvent.Name));
            return thirdPartyEvents;
        }

        public void DoJsonFile(
            Func<List<ThirdPartyEvent>, ThirdPartyEvent, HttpPostedFileBase, ThirdPartyEvent, List<ThirdPartyEvent>> selectedMethod,
            string pathJsonFile,
            ThirdPartyEvent thirdPartyEvent,
            HttpPostedFileBase eventLogoImageData = null,
            ThirdPartyEvent updatedthirdPartyEvent = null)
        {
            _readWriteLockSlim.EnterWriteLock();
            try
            {
                var thirdPartyEvents = GetAllThirdPartyEventsOutoJsonFile(pathJsonFile);
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);

                thirdPartyEvents = selectedMethod(thirdPartyEvents, thirdPartyEvent, eventLogoImageData, updatedthirdPartyEvent);

                SerializeJson(streamWriter, thirdPartyEvents);
            }
            finally
            {
                if (_readWriteLockSlim.IsWriteLockHeld)
                {
                    _readWriteLockSlim.ExitWriteLock();
                }
            }
        }

        public List<ThirdPartyEvent> GetAllThirdPartyEventsOutoJsonFile(string pathJsonFile)
        {
            using var jsonReader = new JsonTextReader(new StreamReader(pathJsonFile));
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<List<ThirdPartyEvent>>(jsonReader);
        }

        private void SerializeJson(StreamWriter streamWriter, object value)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(streamWriter, value);
            streamWriter.Flush();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _readWriteLockSlim.Dispose();
        }

        private string ConvertImageToBase64(HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData is not null)
            {
                var imageByteArray = new byte[eventLogoImageData.ContentLength];
                eventLogoImageData.InputStream.Read(imageByteArray, 0, eventLogoImageData.ContentLength);
                return "data:image/png;base64," + Convert.ToBase64String(imageByteArray);
            }

            return string.Empty;
        }
    }
}