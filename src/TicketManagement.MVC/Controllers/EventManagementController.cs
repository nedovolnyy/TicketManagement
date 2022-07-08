using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.MVC.Models;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "EventManager")]
    public class EventManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventManagementController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> CreateEvent()
        {
            IEnumerable<Venue> venues = await _serviceProvider.GetRequiredService<IVenueService>().GetAllAsync();
            if (venues != null)
            {
                return View(venues);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> EditEvent()
        {
            IEnumerable<Venue> venues = await _serviceProvider.GetRequiredService<IVenueService>().GetAllAsync();
            if (venues is not null)
            {
                return View(venues);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> SelectLayouts(List<string> venuesId)
        {
            List<Layout> layouts = new List<Layout>();
            foreach (var venueId in venuesId)
            {
                layouts.Add(await _serviceProvider.GetRequiredService<ILayoutService>().GetByIdAsync(int.Parse(venueId)));
            }

            return View(layouts);
        }

        [HttpPost]
        public async Task<IActionResult> PublishEvent(EventModel eventModel, string layoutId, string timeZone)
        {
            await _serviceProvider.GetRequiredService<IEventService>().InsertAsync(
                new Event(
                id: 1,
                name: eventModel.Name,
                eventTime: DateTimeOffset.Parse(eventModel.EventTime.ToString()).ToOffset(TimeSpan.Parse(timeZone)),
                description: eventModel.Description,
                eventEndTime: eventModel.EventEndTime,
                eventLogoImage: eventModel.EventLogoImage,
                layoutId: int.Parse(layoutId)),
                price: decimal.Parse(eventModel.Price!));

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public IActionResult InsertEvent(List<string> layoutsId)
        {
            EventModel initEventModel = new EventModel(
                name: "sgdrgdr",
                description: "dffdbd",
                eventTime: DateTime.Now,
                eventLogoImage: "sgsdg",
                eventEndTime: DateTime.Now,
                price: decimal.One.ToString(),
                layoutsId: layoutsId);

            return View(initEventModel);
        }
    }
}