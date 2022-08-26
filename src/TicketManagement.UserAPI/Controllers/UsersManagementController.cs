using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.Identity;

namespace TicketManagement.UserAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Administrator))]
[Produces("application/json")]
public class UsersManagementController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserStore<User> _userStore;
    private readonly IUserEmailStore<User> _emailStore;

    public UsersManagementController(
        RoleManager<Role> roleManager,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserStore<User> userStore)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
    }

    /// <summary>
    /// Returns list of the users.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("users")]
    [AllowAnonymous]
    public List<User> GetAllUsers()
        => _userManager.Users.AsEnumerable().ToList();

    /// <summary>
    /// Add new user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("user")]
    public async Task<IdentityResult> CreateUserAsync(User model, string password)
    {
        model.UserName = model.Email;
        model.NormalizedEmail = string.Format(model.Email).ToUpper();
        model.NormalizedUserName = string.Format(model.Email).ToUpper();

        var user = new User();

        await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            user.FirstName = model.FirstName;
            user.SurName = model.SurName;
            user.PhoneNumber = model.PhoneNumber;
            user.TimeZone = DateTimeOffset.Now.Offset.ToString();
            user.Language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;

            await _userManager.AddToRoleAsync(user, nameof(Roles.User));
            await _userManager.UpdateAsync(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        return result;
    }

    /// <summary>
    /// Update existed user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("user")]
    public async Task<CreateUser> EditUserAsync(CreateUser model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        user.UserName = model.UserName;
        user.FirstName = model.FirstName;
        user.SurName = model.SurName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;

        await _userManager.UpdateAsync(user);
        return model;
    }

    /// <summary>
    /// Delete selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("user/{id}")]
    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            return await _userManager.DeleteAsync(user);
        }

        return IdentityResult.Failed();
    }

    /// <summary>
    /// Returns selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("user/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<User>> GetByIdUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Sets the given <paramref name="userName" /> for the specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user whose name should be set.</param>
    /// <param name="userName">The user name to set.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    [HttpPost("UserName/{userId}")]
    [AllowAnonymous]
    public async Task SetUserNameAsync(string userId, string userName, CancellationToken cancellationToken)
        => await _userStore.SetUserNameAsync(await _userManager.FindByIdAsync(userId), userName, cancellationToken);

    /// <summary>
    /// Gets a flag indicating whether the specified user has a password.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("HasPassword/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> HasPasswordAsync(string userId)
        => await _userManager.HasPasswordAsync(await _userManager.FindByIdAsync(userId));

    /// <summary>
    /// Gets a flag indicating whether the email address for the specified <paramref name="userId"/> has been verified, true if the email address is verified otherwise
    /// false.
    /// </summary>
    /// <param name="userId">The user whose email confirmation status should be returned.</param>
    /// <returns>
    /// The task object containing the results of the asynchronous operation, a flag indicating whether the email address for the specified <paramref name="userId"/>
    /// has been confirmed or not.
    /// </returns>
    [HttpGet("IsEmailConfirmed/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> IsEmailConfirmedAsync(string userId)
        => await _userManager.IsEmailConfirmedAsync(await _userManager.FindByIdAsync(userId));

    /// <summary>
    /// Generates an email change token for the specified user.
    /// </summary>
    /// <param name="userId">The user to generate an email change token for.</param>
    /// <param name="newEmail">The new email address.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, an email change token.
    /// </returns>
    [HttpPost("EmailToken/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> GenerateChangeEmailTokenAsync(string userId, string newEmail)
        => await _userManager.GenerateChangeEmailTokenAsync(await _userManager.FindByIdAsync(userId), newEmail);

    /// <summary>
    /// Returns selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("password/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IdentityResult>> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        => await _userManager.ChangePasswordAsync(await _userManager.FindByIdAsync(userId), currentPassword, newPassword);

    /// <summary>
    /// Signs in the specified <paramref name="userId"/>, whilst preserving the existing
    /// AuthenticationProperties of the current signed-in user like rememberMe, as an asynchronous operation.
    /// </summary>
    /// <param name="userId">The user to sign-in.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    [HttpPost("RefreshSignIn/{userId}")]
    [AllowAnonymous]
    public async Task RefreshSignInAsync(string userId)
        => await _signInManager.RefreshSignInAsync(await _userManager.FindByIdAsync(userId));

    /// <summary>
    /// Returns a flag indicating whether the given <paramref name="password"/> is valid for the
    /// specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user whose password should be validated.</param>
    /// <param name="password">The password to validate.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing true if
    /// the specified <paramref name="password" /> matches the one store for the <paramref name="userId"/>,
    /// otherwise false.</returns>
    [HttpPost("password/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> CheckPasswordAsync(string userId, string password)
        => await _userManager.CheckPasswordAsync(await _userManager.FindByIdAsync(userId), password);

    /// <summary>
    /// Sets the phone number for the specified <paramref name="userId"/>.
    /// </summary>
    /// <param name="userId">The user whose phone number to set.</param>
    /// <param name="phoneNumber">The phone number to set.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="IdentityResult"/>
    /// of the operation.
    /// </returns>
    [HttpPut("PhoneNumber/{userId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IdentityResult>> SetPhoneNumberAsync(string userId, string phoneNumber)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.SetPhoneNumberAsync(user, phoneNumber);
        return await _userManager.UpdateAsync(user);
    }

    /// <summary>
    /// Returns selected users role.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("role/{userId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ChangeRole), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChangeRole>> GetRoleByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var roleModel = new ChangeRole();
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            roleModel = new ChangeRole
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles,
            };
        }

        return roleModel is not null ? Ok(roleModel) : NotFound();
    }

    /// <summary>
    /// Change cart selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("cart")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditCartAsync(string money, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null && money is not null)
        {
            user.Balance += decimal.Parse(Regex.Replace(money, @"[^\d.,]", ""));
            user.PayHistory += "ChangeBalance" + " : $" + user.Balance + " | ";
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        return BadRequest();
    }

    /// <summary>
    /// Purchase methods for selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("purchase")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PurchaseAsync(int eventSeatId, string returnUrl, string price, string userId)
    {
        var invariantPrice = decimal.Parse(price);
        var user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
        {
            user.CartCount++;
            user.Balance -= invariantPrice;
            user.PayHistory += eventSeatId + " : " + returnUrl + Environment.NewLine;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        return BadRequest();
    }

    /// <summary>
    /// Change language selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("language/{userId}")]
    [AllowAnonymous]
    public async Task SetLanguage(string culture, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null && User.Identity.IsAuthenticated)
        {
            user.Language = culture;
            await _userManager.UpdateAsync(user);
        }
    }

    /// <summary>
    /// Change role selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("role/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeRoleAsync(string userId, IEnumerable<string> roles)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return Ok();
        }

        return BadRequest();
    }

    private IUserEmailStore<User> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }

        return (IUserEmailStore<User>)_userStore;
    }
}