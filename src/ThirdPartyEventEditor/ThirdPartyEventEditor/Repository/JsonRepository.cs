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

        public void Insert(ThirdPartyEvent thirdPartyEvent, string pathJsonFile, HttpPostedFileBase eventLogoImageData)
        {
            _readWriteLockSlim.EnterWriteLock();
            try
            {
                thirdPartyEvent.EventLogoImage = ConvertImageToBase64(eventLogoImageData);
                var thirdPartyEvents = GetAllThirdPartyEventsOutoJsonFile(pathJsonFile);
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Add(thirdPartyEvent);
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

        public void Update(ThirdPartyEvent thirdPartyEvent, ThirdPartyEvent updatedthirdPartyEvent, string pathJsonFile, HttpPostedFileBase eventLogoImageData)
        {
            _readWriteLockSlim.EnterWriteLock();
            try
            {
                updatedthirdPartyEvent.EventLogoImage = ConvertImageToBase64(eventLogoImageData);
                var thirdPartyEvents = GetAllThirdPartyEventsOutoJsonFile(pathJsonFile);
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == thirdPartyEvent.Description && x.Name == thirdPartyEvent.Name));
                thirdPartyEvents.Add(updatedthirdPartyEvent);
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

        public void Delete(ThirdPartyEvent thirdPartyEvent, string pathJsonFile)
        {
            _readWriteLockSlim.EnterWriteLock();
            try
            {
                var thirdPartyEvents = GetAllThirdPartyEventsOutoJsonFile(pathJsonFile);
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == thirdPartyEvent.Description && x.Name == thirdPartyEvent.Name));
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
            var imageByteArray = new byte[eventLogoImageData.ContentLength];
            eventLogoImageData.InputStream.Read(imageByteArray, 0, eventLogoImageData.ContentLength);

            return "data:image/png;base64," + Convert.ToBase64String(imageByteArray);
        }
    }
}