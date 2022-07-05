using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Identity;
using TicketManagement.MVC.Views.EventManagement;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "EventManager")]
    public class EventManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventManagementController(UserManager<User> userManager, ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IVenue> venues = await _serviceProvider.GetRequiredService<IVenueService>().GetAllAsync();
            if (venues != null)
            {
                return View(venues);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> SelectLayouts(List<string> venuesId)
        {
            List<ILayout> layouts = new List<ILayout>();
            foreach (var venueId in venuesId)
            {
                layouts.Add(await _serviceProvider.GetRequiredService<ILayoutService>().GetByIdAsync(int.Parse(venueId)));
            }

            return View(layouts);
        }

        [HttpPost]
        public async Task<IActionResult> PlaningEventAreas(List<string> layoutsId)
        {
            List<IArea> areas = new List<IArea>();
            foreach (var layoutId in layoutsId)
            {
                areas.Add(await _serviceProvider.GetRequiredService<IAreaService>().GetByIdAsync(int.Parse(layoutId)));
            }

            return View(areas);
        }

        [HttpPost]
        public IActionResult CreateEvent(List<string> layoutsId)
        {
            CreateEventModel createEventModel = new CreateEventModel(_serviceProvider);
            createEventModel.LayoutId = layoutsId;

            return View(createEventModel);
        }
    }
}