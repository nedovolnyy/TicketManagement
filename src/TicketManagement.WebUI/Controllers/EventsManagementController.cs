using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.WebUI.Models;

namespace TicketManagement.WebUI.Controllers
{
    [Authorize(Roles = "EventManager,Administrator")]
    public class EventsManagementController : Controller
    {
        private readonly EventManagementApiClient _eventManagementApiClient;
        private readonly IServiceProvider _serviceProvider;

        public EventsManagementController(IServiceProvider serviceProvider, EventManagementApiClient eventManagementApiClient)
        {
            _serviceProvider = serviceProvider;
            _eventManagementApiClient = eventManagementApiClient;
        }

        public async Task<IActionResult> SelectVenues()
        {
            var venues = await _serviceProvider.GetRequiredService<IVenueService>().GetAllAsync();
            if (venues != null)
            {
                return View(venues);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> SelectLayouts(IEnumerable<string> venuesId)
        {
            var layouts = new List<Layout>();
            foreach (var venueId in venuesId)
            {
                layouts.Add(await _serviceProvider.GetRequiredService<ILayoutService>().GetByIdAsync(int.Parse(venueId)));
            }

            return View(layouts);
        }

        public IActionResult Insert(List<string> layoutsId)
        {
            var initEventModel = new EventModel(
                name: string.Empty,
                description: string.Empty,
                eventTime: DateTime.Now.AddDays(1),
                eventLogoImage: string.Empty,
                eventEndTime: DateTime.Now.AddDays(1).AddHours(1),
                price: decimal.One,
                layoutsId: layoutsId);

            return View(initEventModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EventModel eventModel, string layoutId, string timeZone, List<string> layoutsId)
        {
            await _eventManagementApiClient.InsertEventAsync(
                price: eventModel.Price,
                new EventManagementApiClientGenerated.Event
                {
                    Name = eventModel.Name,
                    EventTime = DateTimeOffset.Parse(eventModel.EventTime.ToString()).ToOffset(TimeSpan.Parse(timeZone)),
                    Description = eventModel.Description,
                    EventEndTime = eventModel.EventEndTime,
                    EventLogoImage = eventModel.EventLogoImage,
                    LayoutId = int.Parse(layoutId),
                });

            eventModel.LayoutsId = layoutsId;

            if (eventModel.LayoutsId is null || eventModel.LayoutsId.Count < 2)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            eventModel.LayoutsId.Remove(layoutId);

            return View(eventModel);
        }

        public async Task<IActionResult> Edit(int eventId)
        {
            var evnt = await _eventManagementApiClient.GetByIdEventAsync(eventId);
            var layoutId = new List<string>
            {
                evnt.LayoutId.ToString(),
            };
            var editEventModel = new EventModel(
                    name: evnt.Name,
                    description: evnt.Description,
                    eventTime: DateTime.Parse(evnt.EventTime.ToString()),
                    eventLogoImage: evnt.EventLogoImage,
                    eventEndTime: evnt.EventEndTime.DateTime,
                    price: await _eventManagementApiClient.GetPriceByEventIdAsync(eventId),
                    layoutsId: layoutId);
            ViewBag.isAllAvailableSeats = await _eventManagementApiClient.IsAllAvailableSeatsAsync(eventId);
            return View(editEventModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventModel eventModel, int eventId, string timeZone, string layoutId)
        {
            var isAllAvailableSeats = await _eventManagementApiClient.IsAllAvailableSeatsAsync(eventId);
            if (isAllAvailableSeats)
            {
                await _eventManagementApiClient.UpdateEventAsync(
                price: eventModel.Price,
                new EventManagementApiClientGenerated.Event
                {
                    Id = eventId,
                    Name = eventModel.Name,
                    EventTime = DateTimeOffset.Parse(eventModel.EventTime.ToString()).ToOffset(TimeSpan.Parse(timeZone)),
                    Description = eventModel.Description,
                    EventEndTime = eventModel.EventEndTime,
                    EventLogoImage = eventModel.EventLogoImage,
                    LayoutId = int.Parse(layoutId),
                });
            }
            else
            {
                await _eventManagementApiClient.UpdateEventAsync(
                price: decimal.One,
                new EventManagementApiClientGenerated.Event
                {
                    Id = eventId,
                    Name = eventModel.Name,
                    EventTime = DateTimeOffset.Parse(eventModel.EventTime.ToString()).ToOffset(TimeSpan.Parse(timeZone)),
                    Description = eventModel.Description,
                    EventEndTime = eventModel.EventEndTime,
                    EventLogoImage = eventModel.EventLogoImage,
                    LayoutId = int.Parse(layoutId),
                });
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int eventId)
        {
            await _eventManagementApiClient.DeleteEventAsync(eventId);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}