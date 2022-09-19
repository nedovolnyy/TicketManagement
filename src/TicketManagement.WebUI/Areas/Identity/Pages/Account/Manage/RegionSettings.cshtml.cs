using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.WebUI.Helpers;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage;

public class RegionSettingsModel : PageModel
{
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public RegionSettingsModel(UsersManagementApiClient usersManagementApiClient)
    {
        _usersManagementApiClient = usersManagementApiClient;
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
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{User.FindFirstValue(ClaimTypes.NameIdentifier)}'.");
        }

        Load(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string culture, string timeZone)
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{User.FindFirstValue(ClaimTypes.NameIdentifier)}'.");
        }

        if (!ModelState.IsValid)
        {
            Load(user);
            return Page();
        }

        user.SecurityStamp = Guid.NewGuid().ToString();
        user.Language = culture;
        user.TimeZone = timeZone;

        await _usersManagementApiClient.UpdateAsync(user);

        await _usersManagementApiClient.RefreshSignInAsync(user.Id);
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
