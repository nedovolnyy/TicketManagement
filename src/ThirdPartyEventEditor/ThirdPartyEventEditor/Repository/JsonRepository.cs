namespace ThirdPartyEventEditor.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Web;
    using Newtonsoft.Json;
    using ThirdPartyEventEditor.Models;

    public static class JsonRepository
    {
        private static readonly ReaderWriterLockSlim ReadWriteLockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public static void InsertThirdPartyEventToJsonFile(ThirdPartyEvent thirdPartyEvent, string pathJsonFile, HttpPostedFileBase eventLogoImageData)
        {
            thirdPartyEvent.EventLogoImage = ConvertImageToJson(eventLogoImageData);
            var thirdPartyEvents = GetAllThirdPartyEventOutoJsonFile(pathJsonFile);

            ReadWriteLockSlim.EnterWriteLock();
            try
            {
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Add(thirdPartyEvent);
                SerializeJson(streamWriter, thirdPartyEvents);
            }
            finally
            {
                if (ReadWriteLockSlim.IsWriteLockHeld)
                {
                    ReadWriteLockSlim.ExitWriteLock();
                }
            }
        }

        public static void UpdateThirdPartyEventInJsonFile(string jsonThirdPartyEvent, ThirdPartyEvent thirdPartyEvent, string pathJsonFile, HttpPostedFileBase eventLogoImageData)
        {
            thirdPartyEvent.EventLogoImage = ConvertImageToJson(eventLogoImageData);
            var thirdPartyEvents = GetAllThirdPartyEventOutoJsonFile(pathJsonFile);
            var editedThirdPartyEvent = JsonConvert.DeserializeObject<ThirdPartyEvent>(jsonThirdPartyEvent);

            ReadWriteLockSlim.EnterWriteLock();
            try
            {
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == editedThirdPartyEvent.Description && x.Name == editedThirdPartyEvent.Name));
                thirdPartyEvents.Add(thirdPartyEvent);
                SerializeJson(streamWriter, thirdPartyEvents);
            }
            finally
            {
                if (ReadWriteLockSlim.IsWriteLockHeld)
                {
                    ReadWriteLockSlim.ExitWriteLock();
                }
            }
        }

        public static void DeleteThirdPartyEventInJsonFile(string jsonThirdPartyEvent, string pathJsonFile)
        {
            var thirdPartyEvents = GetAllThirdPartyEventOutoJsonFile(pathJsonFile);
            var deletedThirdPartyEvent = JsonConvert.DeserializeObject<ThirdPartyEvent>(jsonThirdPartyEvent);

            ReadWriteLockSlim.EnterWriteLock();
            try
            {
                using var streamWriter = new StreamWriter(pathJsonFile, append: false);
                thirdPartyEvents.Remove(thirdPartyEvents.Find(x => x.Description == deletedThirdPartyEvent.Description && x.Name == deletedThirdPartyEvent.Name));
                SerializeJson(streamWriter, thirdPartyEvents);
            }
            finally
            {
                if (ReadWriteLockSlim.IsWriteLockHeld)
                {
                    ReadWriteLockSlim.ExitWriteLock();
                }
            }
        }

        public static List<ThirdPartyEvent> GetAllThirdPartyEventOutoJsonFile(string pathJsonFile)
        {
            using var jsonReader = new JsonTextReader(new StreamReader(pathJsonFile));
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<List<ThirdPartyEvent>>(jsonReader);
        }

        private static void SerializeJson(StreamWriter streamWriter, object value)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(streamWriter, value);
            streamWriter.Flush();
        }

        private static string ConvertImageToJson(HttpPostedFileBase eventLogoImageData)
        {
            var imageByteArray = new byte[eventLogoImageData.ContentLength];
            eventLogoImageData.InputStream.Read(imageByteArray, 0, eventLogoImageData.ContentLength);

            return "data:image/png;base64," + Convert.ToBase64String(imageByteArray);
        }
    }
}