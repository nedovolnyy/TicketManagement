using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.MVC.Helpers;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "EventManager,Administrator")]
    public class ThirdPartyEventsController : Controller
    {
        private static List<Event> _events = new ();
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ThirdPartyEventsController(IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            _serviceProvider = serviceProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> Insert(string evntName, string evntDescription, string evntTime, string evntEndTime, string evntLogoImage, string evntLayoutId)
        {
            var shortImagePath = "image" + Path.DirectorySeparatorChar + evntName + evntLayoutId + ".png";
            var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, shortImagePath);
            byte[] imgBytes = Convert.FromBase64String(evntLogoImage.Substring(evntLogoImage.LastIndexOf(',') + 1));

            using (var imageFile = new FileStream(fullImagePath, FileMode.Create))
            {
                imageFile.Write(imgBytes, 0, imgBytes.Length);
                imageFile.Flush();
            }

            Event evnt = new ()
            {
                Name = evntName,
                Description = evntDescription,
                EventTime = DateTimeOffset.Parse(evntTime),
                EventEndTime = DateTime.Parse(evntEndTime),
                LayoutId = int.Parse(evntLayoutId),
                EventLogoImage = shortImagePath,
            };

            await _serviceProvider.GetRequiredService<IEventService>().InsertAsync(evnt);

            _events.Remove(_events.Find(x => x.Name == evnt.Name && x.EventTime == evnt.EventTime));
            return View("Preview", _events);
        }

        [HttpPost]
        public ActionResult Delete(string evntName, string evntDescription, string evntTime)
        {
            _events.Remove(_events.Find(x => x.Name == evntName && x.EventTime == DateTimeOffset.Parse(evntTime) && x.Description == evntDescription));
            return View("Preview", _events);
        }

        [HttpPost]
        public async Task<ActionResult> Preview(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            var events = MapEventFromThirdParty(await JsonSerializer.DeserializeAsync<List<ThirdPartyEvent>>(reader.BaseStream));

            return View(events);
        }

        private static List<Event> MapEventFromThirdParty(List<ThirdPartyEvent> thirdPartyEvents)
        {
            _events.Clear();
            foreach (var thirdPartyEvent in thirdPartyEvents)
            {
                Event evnt = new ()
                {
                    Name = thirdPartyEvent.Name,
                    Description = thirdPartyEvent.Description,
                    EventTime = thirdPartyEvent.EventTime,
                    EventEndTime = thirdPartyEvent.EventEndTime,
                    LayoutId = thirdPartyEvent.LayoutId,
                    EventLogoImage = thirdPartyEvent.EventLogoImage,
                };
                _events.Add(evnt);
            }

            return _events;
        }
    }
}
