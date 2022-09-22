using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage;

public class ChangePasswordModel : PageModel
{
    private readonly ILogger<ChangePasswordModel> _logger;
    private readonly UsersManagementApiClient _usersManagementApiClient;
    private readonly string _userId;

    public ChangePasswordModel(
        ILogger<ChangePasswordModel> logger,
        UsersManagementApiClient usersManagementApiClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _usersManagementApiClient = usersManagementApiClient;
        _userId = httpContextAccessor.HttpContext.Request.Cookies["us.id"];
    }

    [BindProperty]
    public InputModel Input { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(_userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userId}'.");
        }

        var hasPassword = await _usersManagementApiClient.HasPasswordAsync(_userId);
        if (!hasPassword)
        {
            return RedirectToPage("./SetPassword");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _usersManagementApiClient.GetByIdUserAsync(_userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userId}'.");
        }

        var changePasswordResult = await _usersManagementApiClient.ChangePasswordAsync(_userId, Input.OldPassword, Input.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        await _usersManagementApiClient.RefreshSignInAsync(_userId);
        _logger.LogInformation("User changed their password successfully.");
        StatusMessage = "Your password has been changed.";

        return RedirectToPage();
    }

    public class InputModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
