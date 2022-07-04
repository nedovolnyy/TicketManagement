// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.Common.Identity;

namespace TicketManagement.MVC.Areas.Identity.Pages.Account.Manage
{
    public class RegionSettingsModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegionSettingsModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public static string TimeZoneCookieName { get; } = "TimeZone";

        public string Language { get; set; }
        public string TimeZone { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private void Load(User user)
        {
            Input = new InputModel
            {
                Language = user.Language,
                TimeZone = user.TimeZone,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string culture, string timeZone)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }

            user.Language = culture;
            user.TimeZone = timeZone;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

            Response.Cookies.Append(TimeZoneCookieName, timeZone,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

            return RedirectToPage();
        }

        public class InputModel
        {
            [Display(Name = "Language")]
            public string Language { get; set; }

            [Display(Name = "Time zone")]
            public string TimeZone { get; set; }
        }
    }
}
