using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage;

public class EmailModel : PageModel
{
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public EmailModel(UsersManagementApiClient usersManagementApiClient)
    {
        _usersManagementApiClient = usersManagementApiClient;
    }

    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    private async Task LoadAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        Email = user.Email;

        Input = new InputModel
        {
            NewEmail = user.Email,
        };

        IsEmailConfirmed = await _usersManagementApiClient.IsEmailConfirmedAsync(user.Id);
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

    public async Task<IActionResult> OnPostChangeEmailAsync()
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

        if (Input.NewEmail != user.Email)
        {
            var code = await _usersManagementApiClient.GenerateChangeEmailTokenAsync(user.Id, Input.NewEmail);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmailChange",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, email = Input.NewEmail, code = code },
                protocol: Request.Scheme);

            StatusMessage = $"Your email is changed.";
            return Redirect(HtmlEncoder.Default.Encode(callbackUrl));
        }

        StatusMessage = "Your email is unchanged.";
        return RedirectToPage();
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }
    }
}
