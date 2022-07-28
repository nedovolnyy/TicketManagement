namespace ThirdPartyEventEditor.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using log4net;
    using ThirdPartyEventEditor.Models;
    using ThirdPartyEventEditor.Repository;

    public class ThirdPartyEventsController : Controller
    {
        private readonly JsonRepository _jsonRepository = new JsonRepository();
        private readonly string _jsonFileName = ConfigurationManager.AppSettings["JsonFileName"];
        private readonly ILog _logger;

        public ThirdPartyEventsController(ILog logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            var events = _jsonRepository.GetAllThirdPartyEventsOutoJsonFile(GetPath(_jsonFileName));
            _logger.Debug("Deserialized all ThirdPartyEvents from .json file");

            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(ThirdPartyEvent newThirdPartyEvent, HttpPostedFileBase eventLogoImageData)
        {
            _jsonRepository.DoJsonFile(_jsonRepository.Insert, GetPath(_jsonFileName), newThirdPartyEvent, eventLogoImageData);
            _logger.Debug("Added new ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ThirdPartyEvent thirdPartyEvent, ThirdPartyEvent updatedThirdPartyEvent, HttpPostedFileBase eventLogoImageData)
        {
            _jsonRepository.DoJsonFile(_jsonRepository.Update, GetPath(_jsonFileName), thirdPartyEvent, eventLogoImageData, updatedThirdPartyEvent);
            _logger.Debug("Updated existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ThirdPartyEvent thirdPartyEvent)
        {
            _jsonRepository.DoJsonFile(_jsonRepository.Delete, GetPath(_jsonFileName), thirdPartyEvent);
            _logger.Debug("Deleted existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        private string GetPath(string filename)
        {
            return Path.Combine(Server.MapPath("~/App_Data/"), filename);
        }
    }
}