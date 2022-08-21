using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.Common.Identity;
using TicketManagement.WebUI.Client;
using TicketManagement.WebUI.Helpers;

namespace TicketManagement.WebUI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRestClient _userRestClient;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(UserManager<User> userManager, IUserRestClient userRestClient, SignInManager<User> signInManager, ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRestClient = userRestClient;
            _logger = logger;
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

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                ////var result = await _signInManager.LoginAsync(
                ////    new UserApiClientGenerated.LoginModel
                ////        {
                ////            Email = Input.Email,
                ////            Password = Input.Password,
                ////            RememberMe = Input.RememberMe,
                ////        });

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    ////var token = await Login(_userRestClient, new UserModel
                    ////{
                    ////    Login = Input.Email,
                    ////    Password = Input.Password,
                    ////});
                    ////HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                    ////{
                    ////    HttpOnly = true,
                    ////    SameSite = SameSiteMode.Strict,
                    ////});

                    HtmlHelperExtensions.SaveUserCookies(Response, user);

                    return LocalRedirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            return Page();
        }

        ////private async Task<string> Login(IUserRestClient userClient, UserModel userModel, CancellationToken cancellationToken = default)
        ////{
        ////    var form = new MultipartFormDataContent
        ////    {
        ////        { new StringContent(userModel.Login), nameof(UserModel.Login) },
        ////        { new StringContent(userModel.Password), nameof(UserModel.Password) },
        ////    };
        ////    var result = await userClient.Login(form, cancellationToken);
        ////    return result;
        ////}

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
}
