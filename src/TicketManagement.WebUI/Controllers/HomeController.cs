﻿using System.Diagnostics;
using System.Text.RegularExpressions;
////using EventApiClientGenerated;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.WebUI.Models;

namespace TicketManagement.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ////private readonly EventApiClient _eventApiClient;
        private readonly UserManager<User> _userManager;
        private readonly IServiceProvider _serviceProvider;
        public HomeController(UserManager<User> userManager, IServiceProvider serviceProvider/*, EventApiClient eventApiClient*/)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            ////_eventApiClient = eventApiClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _serviceProvider.GetRequiredService<IEventService>().GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event evnt)
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
                user.PayHistory += "ChangeBalance" + " : $" + user.Balance + " | ";
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> NoBalance()
        {
            return View(await _userManager.GetUserAsync(User));
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