using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.MVC.Models;

namespace TicketManagement.MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventController(UserManager<User> userManager, ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index(int eventId)
        {
            if (eventId != default)
            {
                IEnumerable<IEventArea> eventAreas = await _serviceProvider.GetRequiredService<IEventAreaService>().GetAllByEventIdAsync(eventId);
                if (eventAreas != null)
                {
                    return View(eventAreas);
                }

                return NotFound();
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public IActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(int eventSeatId, string returnUrl, string price)
        {
            if (User is not null)
            {
                var invariantPrice = decimal.Parse(price);
                User user = await _userManager.GetUserAsync(User);
                if (user.Balance < invariantPrice)
                {
                    return RedirectToRoute(new { controller = "Home", action = "GetMoney" });
                }
                else
                {
                    user.CartCount++;
                    user.Balance -= invariantPrice;
                    user.PayHistory += eventSeatId + " : " + returnUrl + Environment.NewLine;
                    await _userManager.UpdateAsync(user);
                    await _serviceProvider.GetRequiredService<IEventSeatService>().ChangeEventSeatStatusAsync(eventSeatId);
                }
            }

            return LocalRedirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}