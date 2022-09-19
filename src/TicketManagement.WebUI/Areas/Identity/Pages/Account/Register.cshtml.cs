using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TicketManagement.Common;
using TicketManagement.Common.JwtTokenAuth;
using TicketManagement.WebUI.Helpers;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account;

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
            var user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = Input.Email,
                UserName = Input.Email,
            };

            var result = await _usersManagementApiClient.CreateUserAsync(Input.Password, user);

            if (result.Succeeded)
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.TimeZone = DateTimeOffset.Now.Offset.ToString();
                user.Language = PageContext.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

                await _usersManagementApiClient.AddToRoleAsync(user.Email, "User");
                await _usersManagementApiClient.UpdateAsync(user);
                await _usersManagementApiClient.SignInAsync(user.Email, isPersistent: false);
                _logger.LogInformation("User created a new account with password.");
                var authenticationResult = await GetAuthenticationResultAfterSignIn();
                if (authenticationResult.Result)
                {
                    HttpContext.Response.Cookies.Append("token", authenticationResult.Token);
                    var userRole = await _usersManagementApiClient.GetRoleByIdAsync(authenticationResult.User.Id);
                    _logger.LogInformation("User logged in.");

                    var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, authenticationResult.User.Id),
                    new Claim(ClaimTypes.Email, Input.Email),
                    new Claim(ClaimTypes.Name, Input.Email),
                };
                    userClaims.AddRange(userRole.UserRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

                    var claimsIdentity = new ClaimsIdentity(userClaims, Settings.Jwt.JwtOrCookieScheme);
                    await HttpContext.SignInAsync(Settings.Jwt.JwtOrCookieScheme, new ClaimsPrincipal(claimsIdentity));
                    HtmlHelperExtensions.SaveUserCookies(Response, new User
                    {
                        Id = authenticationResult.User.Id,
                        Language = "en-US",
                        TimeZone = authenticationResult.User.TimeZone,
                    });

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in authenticationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }

    private async Task<AuthenticationResult> GetAuthenticationResultAfterSignIn()
    {
        using var httpClient = new HttpClient();
        var stringContent = new StringContent(JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");
        using var response = await httpClient.PostAsync($"https://localhost:5004/api/users/login", stringContent);
        var authenticationResultJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AuthenticationResult>(authenticationResultJson);
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
