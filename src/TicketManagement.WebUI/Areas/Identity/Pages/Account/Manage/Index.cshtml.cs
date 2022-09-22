using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public IndexModel(UsersManagementApiClient usersManagementApiClient)
    {
        _usersManagementApiClient = usersManagementApiClient;
    }

    [Display(Name = "Username")]
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SurName { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    private async Task LoadAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

        Username = user.UserName;
        Input = new InputModel
        {
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            SurName = user.SurName,
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{User.FindFirstValue(ClaimTypes.NameIdentifier)}'.");
        }

        await LoadAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{User.FindFirstValue(ClaimTypes.NameIdentifier)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        user.SecurityStamp=Guid.NewGuid().ToString();
        user.FirstName = Input.FirstName;
        user.SurName = Input.SurName;
        await _usersManagementApiClient.UpdateAsync(user);

        if (Input.PhoneNumber != user.PhoneNumber)
        {
            var setPhoneResult = await _usersManagementApiClient.SetPhoneNumberAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), Input.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set phone number.";
                return RedirectToPage();
            }
        }

        await _usersManagementApiClient.RefreshSignInAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        StatusMessage = "Your profile has been updated";
        return RedirectToPage();
    }

    public class InputModel
    {
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "SurName")]
        public string SurName { get; set; }
    }
}
