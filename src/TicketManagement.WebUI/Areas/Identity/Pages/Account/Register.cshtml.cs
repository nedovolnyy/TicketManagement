using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly UsersManagementApiClient _usersManagementApiClient;

        public RegisterModel(
            ILogger<RegisterModel> logger,
            UsersManagementApiClient usersManagementApiClient)
        {
            _logger = logger;
            _usersManagementApiClient = usersManagementApiClient;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User();

                await _usersManagementApiClient.SetUserNameAsync(user.Id, Input.Email, CancellationToken.None);
                var result = await _usersManagementApiClient.CreateUserAsync(Input.Password, user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
