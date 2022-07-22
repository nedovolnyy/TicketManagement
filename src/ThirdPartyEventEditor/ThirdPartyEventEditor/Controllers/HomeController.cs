using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThirdPartyEventEditor.Models;

namespace ThirdPartyEventEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _jsonFileName = ConfigurationManager.AppSettings["JsonFileName"];
        public ActionResult Index()
        {
            // GetAll
            using FileStream fs = new(GetPath(_jsonFileName), FileMode.OpenOrCreate);
            var events = Deserialize<List<ThirdPartyEvent>>(fs);

            ////HttpClient httpClient = new ();
            ////object locker = new();
            ////lock (locker)
            ////{
            ////}

            return View(events);
        }

        private string ConvertImageToJson(byte[] stream)
        {
            return "data:image/png;base64," + Convert.ToBase64String(stream);
        }

        private string GetPath(string filename)
        {
            return Path.Combine(Server.MapPath("~/App_Data/"), filename);
        }

        [HttpPost]
        public ActionResult Insert(string name, string eventTime, string eventEndTime, string description, string layoutId, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData != null)
            {
                byte[] imageByteArr = new byte[eventLogoImageData.ContentLength];
                eventLogoImageData.InputStream.Read(imageByteArr, 0, eventLogoImageData.ContentLength);

                ThirdPartyEvent newEvent = new()
                {
                    Name = name,
                    EventTime = DateTimeOffset.Parse(eventTime),
                    EventEndTime = DateTime.Parse(eventEndTime),
                    LayoutId = int.Parse(layoutId),
                    Description = description,
                    EventLogoImage = ConvertImageToJson(imageByteArr),
                };
                using FileStream fs = new(GetPath(_jsonFileName), FileMode.OpenOrCreate);

                using StreamReader reader = new(fs);
                using JsonTextReader jsonReader = new(reader);
                var serializer = new JsonSerializer();
                var events = serializer.Deserialize<List<ThirdPartyEvent>>(jsonReader);
                lock (fs)
                {
                    fs.SetLength(0);
                }
                events.Add(newEvent);
                Serialize(fs, events);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(string evnt, string name, string eventTime, string eventEndTime, string description, string layoutId, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData != null)
            {
                byte[] imageByteArr = new byte[eventLogoImageData.ContentLength];
                eventLogoImageData.InputStream.Read(imageByteArr, 0, eventLogoImageData.ContentLength);

            ThirdPartyEvent newEvent = new()
            {
                Name = name,
                EventTime = DateTimeOffset.Parse(eventTime),
                EventEndTime = DateTime.Parse(eventEndTime),
                LayoutId = int.Parse(layoutId),
                Description = description,
                EventLogoImage = ConvertImageToJson(imageByteArr),
            };
            using FileStream fs = new(GetPath(_jsonFileName), FileMode.OpenOrCreate);

            using StreamReader reader = new(fs);
            using JsonTextReader jsonReader = new(reader);
            var serializer = new JsonSerializer();
            var events = serializer.Deserialize<List<ThirdPartyEvent>>(jsonReader);
            lock (fs)
            {
                fs.SetLength(0);
            }
            var tempEvent = JsonConvert.DeserializeObject<ThirdPartyEvent>(evnt);
            var ind = events.Find(x => x.Description == tempEvent.Description && x.Name == tempEvent.Name);
            events.Remove(ind);
            events.Add(newEvent);
            Serialize(fs, events);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string evnt)
        {
            using FileStream fs = new(GetPath(_jsonFileName), FileMode.OpenOrCreate);
            using StreamReader reader = new(fs);
            using JsonTextReader jsonReader = new(reader);
            var serializer = new JsonSerializer();
            var events = serializer.Deserialize<List<ThirdPartyEvent>>(jsonReader);
            lock (fs)
            {
                fs.SetLength(0);
            }

            var tempEvent = JsonConvert.DeserializeObject<ThirdPartyEvent>(evnt);
            var ind = events.Find(x => x.Description == tempEvent.Description && x.Name == tempEvent.Name);
            events.Remove(ind);
            Serialize(fs, events);

            return RedirectToAction("Index");
        }

        private static void Serialize(Stream s, object value)
        {
            using StreamWriter writer = new(s);
            using JsonTextWriter jsonWriter = new(writer);
            JsonSerializer ser = new();
            ser.Serialize(jsonWriter, value);
            jsonWriter.Flush();
        }

        private static T Deserialize<T>(Stream s)
        {
            using StreamReader reader = new(s);
            using JsonTextReader jsonReader = new(reader);
            JsonSerializer ser = new();
            return ser.Deserialize<T>(jsonReader);
        }
    }
}