using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.Common.Identity;
using TicketManagement.WebUI.Helpers;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage
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

            HtmlHelperExtensions.SaveUserCookies(Response, user);

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
