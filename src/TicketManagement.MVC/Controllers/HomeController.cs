using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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

        public async Task<IActionResult> Cart()
        {
            return View(await _userManager.GetUserAsync(User));
        }

        [HttpPost]
        public async Task<IActionResult> Cart(string money)
        {
            if (User is not null && money is not null)
            {
                User user = await _userManager.GetUserAsync(User);
                user.Balance += decimal.Parse(Regex.Replace(money, @"[^\d.,]", ""));
                user.PayHistory += "AddMoney" + " : " + user.Balance + Environment.NewLine;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("GetMoney");
        }

        public async Task<IActionResult> GetMoney()
        {
            return View(await _userManager.GetUserAsync(User));
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