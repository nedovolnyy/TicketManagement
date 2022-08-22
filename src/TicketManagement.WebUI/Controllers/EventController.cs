using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.WebUI.Models;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly UsersManagementApiClient _usersManagementApiClient;

        public EventController(ILogger<HomeController> logger, IServiceProvider serviceProvider, UsersManagementApiClient usersManagementApiClient)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _usersManagementApiClient = usersManagementApiClient;
        }

        public async Task<IActionResult> Index(int eventId)
        {
            if (eventId != default)
            {
                IEnumerable<EventArea> eventAreas = await _serviceProvider.GetRequiredService<IEventAreaService>().GetAllByEventIdAsync(eventId);
                if (eventAreas != null)
                {
                    return View(eventAreas);
                }

                return NotFound();
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(int eventSeatId, string returnUrl, string price)
        {
            var invariantPrice = decimal.Parse(price);
            var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (User.Identity.IsAuthenticated)
            {
                if (user.Balance < invariantPrice)
                {
                    return RedirectToRoute(new { controller = "Home", action = "NoBalance" });
                }
                else
                {
                    await _usersManagementApiClient.PurchaseAsync(eventSeatId, returnUrl, price, User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await _serviceProvider.GetRequiredService<IEventSeatService>().ChangeEventSeatStatusAsync(eventSeatId);
                }
            }

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}