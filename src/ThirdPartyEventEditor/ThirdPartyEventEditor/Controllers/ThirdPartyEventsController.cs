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
        private readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            _jsonRepository.Insert(newThirdPartyEvent, GetPath(_jsonFileName), eventLogoImageData);
            _logger.Debug("Added new ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ThirdPartyEvent thirdPartyEvent, ThirdPartyEvent updatedThirdPartyEvent, HttpPostedFileBase eventLogoImageData)
        {
            _jsonRepository.Update(thirdPartyEvent, updatedThirdPartyEvent, GetPath(_jsonFileName), eventLogoImageData);
            _logger.Debug("Updated existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ThirdPartyEvent thirdPartyEvent)
        {
            _jsonRepository.Delete(thirdPartyEvent, GetPath(_jsonFileName));
            _logger.Debug("Deleted existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        private string GetPath(string filename)
        {
            return Path.Combine(Server.MapPath("~/App_Data/"), filename);
        }
    }
}