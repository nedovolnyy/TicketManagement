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
        private ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            var events = JsonRepository.GetAllThirdPartyEventOutoJsonFile(this.GetPath(this._jsonFileName));
            this._logger.Debug("Deserialized all ThirdPartyEvents from .json file");

            return this.View(events);
        }

        [HttpPost]
        public ActionResult Insert(string name, string eventTime, string eventEndTime, string description, string layoutId, string price, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData is not null &&
                name != string.Empty &&
                eventTime is not null &&
                eventEndTime is not null &&
                description != string.Empty &&
                layoutId != string.Empty &&
                price != string.Empty)
            {
                var newThirdPartyEvent = new ThirdPartyEvent()
                {
                    Name = name,
                    EventTime = DateTimeOffset.Parse(eventTime),
                    EventEndTime = DateTime.Parse(eventEndTime),
                    LayoutId = int.Parse(layoutId),
                    Description = description,
                    EventLogoImage = string.Empty,
                    Price = decimal.Parse(price),
                };

                JsonRepository.InsertThirdPartyEventToJsonFile(newThirdPartyEvent, this.GetPath(this._jsonFileName), eventLogoImageData);
                this._logger.Debug("Added new ThirdPartyEvent into .json file");
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(string thirdPartyEvent, string name, string eventTime, string eventEndTime, string description, string layoutId, string price, HttpPostedFileBase eventLogoImageData)
        {
            if (eventLogoImageData is not null &&
                name != string.Empty &&
                eventTime is not null &&
                eventEndTime is not null &&
                description != string.Empty &&
                layoutId != string.Empty &&
                price != string.Empty)
            {
                var newThirdPartyEvent = new ThirdPartyEvent()
                {
                    Name = name,
                    EventTime = DateTimeOffset.Parse(eventTime),
                    EventEndTime = DateTime.Parse(eventEndTime),
                    LayoutId = int.Parse(layoutId),
                    Description = description,
                    EventLogoImage = string.Empty,
                    Price = decimal.Parse(price),
                };

                JsonRepository.UpdateThirdPartyEventInJsonFile(thirdPartyEvent, newThirdPartyEvent, this.GetPath(this._jsonFileName), eventLogoImageData);
                this._logger.Debug("Updated existing ThirdPartyEvent into .json file");
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string thirdPartyEvent)
        {
            JsonRepository.DeleteThirdPartyEventInJsonFile(thirdPartyEvent, this.GetPath(this._jsonFileName));
            this._logger.Debug("Deleted existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        private string GetPath(string filename)
        {
            return Path.Combine(this.Server.MapPath("~/App_Data/"), filename);
        }
    }
}