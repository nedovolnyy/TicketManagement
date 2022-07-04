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
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceProvider _serviceProvider;
        public HomeController(UserManager<User> userManager, ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Name = User.Identity?.Name;
            ViewBag.IsAuthenticated = User.Identity?.IsAuthenticated;
            return View(await _serviceProvider.GetRequiredService<IEventService>().GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IEvent evnt)
        {
            await _serviceProvider.GetRequiredService<IEventService>().InsertAsync(evnt);
            return RedirectToAction("Index");
        }

        public IActionResult Event()
        {
            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Language = culture;
                await _userManager.UpdateAsync(user);
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

            return LocalRedirect(returnUrl);
        }
    }
}