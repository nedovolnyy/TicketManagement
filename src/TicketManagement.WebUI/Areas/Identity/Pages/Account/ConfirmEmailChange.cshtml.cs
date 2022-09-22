using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account;

public class ConfirmEmailChangeModel : PageModel
{
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public ConfirmEmailChangeModel(UsersManagementApiClient usersManagementApiClient)
    {
        _usersManagementApiClient = usersManagementApiClient;
    }

    [TempData]
    public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
    {
        if (userId == null || email == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _usersManagementApiClient.GetByIdUserAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _usersManagementApiClient.ChangeEmailAsync(user.Id, email, code);
        if (!result.Succeeded)
        {
            StatusMessage = "Error changing email.";
            return Page();
        }

        await _usersManagementApiClient.SetUserNameAsync(user.Id, email);

        await _usersManagementApiClient.RefreshSignInAsync(user.Id);
        StatusMessage = "Thank you for confirming your email change.";
        return Page();
    }
}
