using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThirdPartyEventEditor.Models;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "EventManager,Administrator")]
    public class ThirdPartyEventsController : Controller
    {
        private static readonly List<ThirdPartyEvent> _thirdPartyEvents = new List<ThirdPartyEvent>();
        private readonly IThirdPartyEventService _thirdPartyEventService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ThirdPartyEventsController(IThirdPartyEventService thirdPartyEventService, IWebHostEnvironment webHostEnvironment)
        {
            _thirdPartyEventService = thirdPartyEventService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> Add(Event @event, decimal thirdPartyEventPrice)
        {
            var shortImagePath = "image" + Path.DirectorySeparatorChar + @event.Name + @event.LayoutId + ".png";
            var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, shortImagePath);

            await _thirdPartyEventService.InsertAsync(fullImagePath, @event, thirdPartyEventPrice, @event.EventLogoImage);

            _thirdPartyEvents.Remove(_thirdPartyEvents.Find(x => x.Name == @event.Name && x.EventTime == @event.EventTime));
            return View("Preview", _thirdPartyEvents);
        }

        [HttpPost]
        public ActionResult Delete(string thirdPartyEventName, string thirdPartyEventDescription, string thirdPartyEventTime)
        {
            _thirdPartyEvents.Remove(
                _thirdPartyEvents.Find(x => x.Name == thirdPartyEventName && x.EventTime == DateTimeOffset.Parse(thirdPartyEventTime) && x.Description == thirdPartyEventDescription));
            return View("Preview", _thirdPartyEvents);
        }

        [HttpPost]
        public async Task<ActionResult> Preview(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            var thirdPartyEvents = PrepareListOfThirdPartyEvents(await JsonSerializer.DeserializeAsync<List<ThirdPartyEvent>>(reader.BaseStream));

            return View(thirdPartyEvents);
        }

        private List<ThirdPartyEvent> PrepareListOfThirdPartyEvents(List<ThirdPartyEvent> thirdPartyEvents)
        {
            _thirdPartyEvents.Clear();
            foreach (var thirdPartyEvent in thirdPartyEvents)
            {
                _thirdPartyEvents.Add(thirdPartyEvent);
            }

            return _thirdPartyEvents;
        }
    }
}
