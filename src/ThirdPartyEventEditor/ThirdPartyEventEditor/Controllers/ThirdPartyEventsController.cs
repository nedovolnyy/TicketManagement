namespace ThirdPartyEventEditor.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using log4net;
    using ThirdPartyEventEditor.Models;
    using ThirdPartyEventEditor.Repository;

    public class ThirdPartyEventsController : Controller
    {
        private readonly JsonRepository _jsonRepository;
        private readonly ILog _logger;

        public ThirdPartyEventsController(ILog logger, JsonRepository jsonRepository)
        {
            _logger = logger;
            _jsonRepository = jsonRepository;
        }

        public ActionResult Index()
        {
            var events = _jsonRepository.GetAllThirdPartyEventsOutoJsonFile();
            _logger.Debug("Deserialized all ThirdPartyEvents from .json file");

            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(ThirdPartyEvent newThirdPartyEvent, HttpPostedFileBase eventLogoImageData)
        {
            _jsonRepository.Insert(newThirdPartyEvent, eventLogoImageData);
            _logger.Debug("Added new ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ThirdPartyEvent thirdPartyEvent, HttpPostedFileBase eventLogoImageData, ThirdPartyEvent updatedThirdPartyEvent)
        {
            _jsonRepository.Update(thirdPartyEvent, eventLogoImageData, updatedThirdPartyEvent);
            _logger.Debug("Updated existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ThirdPartyEvent thirdPartyEvent)
        {
            _jsonRepository.Delete(thirdPartyEvent);
            _logger.Debug("Deleted existing ThirdPartyEvent into .json file");

            return RedirectToAction("Index");
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}