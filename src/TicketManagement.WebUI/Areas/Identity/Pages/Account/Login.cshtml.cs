using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TicketManagement.Common;
using TicketManagement.Common.JwtTokenAuth;
using TicketManagement.WebUI.Helpers;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public LoginModel(ILogger<LoginModel> logger, UsersManagementApiClient usersManagementApiClient)
    {
        _logger = logger;
        _usersManagementApiClient = usersManagementApiClient;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("~/");

        await HttpContext.SignOutAsync(Settings.Jwt.JwtOrCookieScheme);

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var authenticationResult = await GetAuthenticationResultAfterSignIn();
            HttpContext.Response.Cookies.Append("token", authenticationResult.Token);
            var userRole = await _usersManagementApiClient.GetRoleByIdAsync(authenticationResult.User.Id);
            if (authenticationResult.Result)
            {
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
                    Language = "en-US",
                    TimeZone = authenticationResult.User.TimeZone,
                });

                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }

        return RedirectToPage();
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
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
