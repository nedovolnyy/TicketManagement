using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.Identity;
using TicketManagement.UserAPI.Models;
using TicketManagement.UserAPI.Responses;
using TicketManagement.UserAPI.Services;

namespace TicketManagement.UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;

        public UsersController(
            UserManager<User> userManager,
            JwtTokenService jwtTokenService,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            _logger.LogInformation("Trying to register a new user.");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid payload was provided.");

                return BadRequest(new AuthenticationResult
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Invalid payload",
                    },
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                _logger.LogInformation("The user with provided email already exists.");

                return BadRequest(new AuthenticationResult
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Email already exists",
                    },
                });
            }

            var newUser = new User { Email = model.Email, UserName = model.Email };
            var isCreated = await _userManager.CreateAsync(newUser, model.Password);
            if (!isCreated.Succeeded)
            {
                _logger.LogError("Something went wrong while creating a new user.");

                return new JsonResult(new AuthenticationResult
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                })
                { StatusCode = 500 };
            }

            ////if (model.IsAdmin)
            ////{
            ////    _logger.LogInformation($"The user with name {newUser.UserName} was assigned to role Admin.");

            ////    await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            ////}

            ////var roleName = model.IsAdmin ? UserRoles.Admin : UserRoles.Manager;
            var roleName = await _userManager.GetRolesAsync(newUser);
            var jwtToken = _jwtTokenService.GenerateJwtToken(newUser, roleName);

            _logger.LogInformation($"The user with name {model.Email} was registered.");

            return Ok(new AuthenticationResult
            {
                Result = true,
                Token = jwtToken,
            });
        }

        /// <summary>
        /// Login into the new API.
        /// </summary>
        /// <remarks>
        /// Here is the code sample of usage.
        /// </remarks>
        /// <param name="model">.</param>
        /// <returns>..</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation($"Trying to login with user email {model.Email}.");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid payload was provided.");

                return BadRequest(new AuthenticationResult
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Invalid payload",
                    },
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser == null)
            {
                _logger.LogInformation("The user with provided email had not been found.");

                return BadRequest(new AuthenticationResult
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "The user with this email does not exist.",
                    },
                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);
            ////var roleName = await _userManager.GetRolesAsync(existingUser);

            if (!isCorrect)
            {
                _logger.LogInformation($"The provided password is not correct for user {existingUser.UserName}.");

                return BadRequest(new AuthenticationResult
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Password is not correct.",
                    },
                });
            }

            ////var isAdmin = roles.Contains(UserRoles.Admin);
            ////var roleName = isAdmin ? UserRoles.Admin : UserRoles.Manager;
            var roleName = await _userManager.GetRolesAsync(existingUser);
            var jwtToken = _jwtTokenService.GenerateJwtToken(existingUser, roleName);

            _logger.LogInformation($"The user with email {model.Email} was logged in.");

            return Ok(new AuthenticationResult
            {
                Result = true,
                Token = jwtToken,
            });
        }

        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            return _jwtTokenService.ValidateToken(token) ? Ok() : Forbid();
        }

        ////private async Task InitAdminRoleIfNotExists()
        ////{
        ////    if (!await _roleManager.RoleExistsAsync("Admin"))
        ////    {
        ////        var role = new IdentityRole
        ////        {
        ////            Name = "Admin",
        ////        };
        ////        await _roleManager.CreateAsync(role);
        ////    }
        ////}
    }
}
