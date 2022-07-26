namespace ThirdPartyEventEditor.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using log4net;
    using ThirdPartyEventEditor.Models;
    using ThirdPartyEventEditor.Repository;

    public class HomeController : Controller
    {
        private readonly string _jsonFileName = ConfigurationManager.AppSettings["JsonFileName"];
        private readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            var events = JsonRepository.GetAllThirdPartyEventOutoJsonFile(GetPath(_jsonFileName));
            _logger.Debug("Deserialized all ThirdPartyEvents from .json file");

            return View(events);
        }

        [HttpPost]
        public ActionResult InsertThirdPartyEvent(string name, string eventTime, string eventEndTime, string description, string layoutId, string price, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData is not null &
                name != string.Empty &
                eventTime is not null &
                eventEndTime is not null &
                description != string.Empty &
                layoutId != string.Empty &
                price != string.Empty)
            {
                var newThirdPartyEvent = new ThirdPartyEvent
                {
                    Name = name,
                    EventTime = DateTimeOffset.Parse(eventTime),
                    EventEndTime = DateTime.Parse(eventEndTime),
                    LayoutId = int.Parse(layoutId),
                    Description = description,
                    EventLogoImage = string.Empty,
                    Price = decimal.Parse(price),
                };

                JsonRepository.InsertThirdPartyEventToJsonFile(newThirdPartyEvent, GetPath(_jsonFileName), eventLogoImageData);
                _logger.Debug("Added new ThirdPartyEvent into .json file");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateThirdPartyEvent(
            string thirdPartyEvent, string name, string eventTime,
            string eventEndTime, string description, string layoutId,
            string price, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData is not null &
                name != string.Empty &
                eventTime is not null &
                eventEndTime is not null &
                description != string.Empty &
                layoutId != string.Empty &
                price != string.Empty)
            {
                var newThirdPartyEvent = new ThirdPartyEvent
                {
                    Name = name,
                    EventTime = DateTimeOffset.Parse(eventTime),
                    EventEndTime = DateTime.Parse(eventEndTime),
                    LayoutId = int.Parse(layoutId),
                    Description = description,
                    EventLogoImage = string.Empty,
                    Price = decimal.Parse(price),
                };

                JsonRepository.UpdateThirdPartyEventInJsonFile(thirdPartyEvent, newThirdPartyEvent, GetPath(_jsonFileName), eventLogoImageData);
                _logger.Debug("Updated existing ThirdPartyEvent into .json file");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteThirdPartyEvent(string thirdPartyEvent)
        {
            JsonRepository.DeleteThirdPartyEventInJsonFile(thirdPartyEvent, GetPath(_jsonFileName));
            _logger.Debug("Deleted existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        private string GetPath(string filename)
        {
            return Path.Combine(Server.MapPath("~/App_Data/"), filename);
        }
    }
}