using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.Common;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account.Manage;

public class DeletePersonalDataModel : PageModel
{
    private readonly ILogger<DeletePersonalDataModel> _logger;
    private readonly UsersManagementApiClient _usersManagementApiClient;
    private readonly string _userId;

    public DeletePersonalDataModel(
        ILogger<DeletePersonalDataModel> logger,
        UsersManagementApiClient usersManagementApiClient)
    {
        _logger = logger;
        _usersManagementApiClient = usersManagementApiClient;
        _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public bool RequirePassword { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(_userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userId}'.");
        }

        RequirePassword = await _usersManagementApiClient.HasPasswordAsync(_userId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(_userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userId}'.");
        }

        RequirePassword = await _usersManagementApiClient.HasPasswordAsync(_userId);
        if (RequirePassword && !await _usersManagementApiClient.CheckPasswordAsync(_userId, Input.Password))
        {
            ModelState.AddModelError(string.Empty, "Incorrect password.");
            return Page();
        }

        await _usersManagementApiClient.DeleteUserAsync(_userId);

        await HttpContext.SignOutAsync(Settings.Jwt.JwtOrCookieScheme);
        Response.Cookies.Delete("token");

        _logger.LogInformation("User with ID '{UserId}' deleted themselves.", _userId);

        return Redirect("~/");
    }

    public class InputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
