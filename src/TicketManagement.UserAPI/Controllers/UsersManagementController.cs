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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
[Produces("application/json")]
public class UsersManagementController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IUserStore<User> _userStore;
    private readonly IUserEmailStore<User> _emailStore;

    public UsersManagementController(
        RoleManager<Role> roleManager,
        UserManager<User> userManager,
        IUserStore<User> userStore)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _userStore = userStore;
        _emailStore = GetEmailStore();
    }

    /// <summary>
    /// Returns list of the users.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        var users = _userManager.Users.ToList();
        return users is null ? NotFound() : Ok(users);
    }

    /// <summary>
    /// Add new user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IdentityResult>> CreateUserAsync(CreateUser model)
    {
        model.UserName = model.Email;
        model.NormalizedEmail = string.Format(model.Email).ToUpper();
        model.NormalizedUserName = string.Format(model.Email).ToUpper();

        var user = new User();

        await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            user.FirstName = model.FirstName;
            user.SurName = model.SurName;
            user.PhoneNumber = model.PhoneNumber;
            user.TimeZone = DateTimeOffset.Now.Offset.ToString();
            user.Language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;

            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.UpdateAsync(user);
        }

        return result;
    }

    /// <summary>
    /// Update existed user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut]
    public async Task<ActionResult<CreateUser>> EditUserAsync(CreateUser model)
    {
        User user = await _userManager.FindByIdAsync(model.Id);

        user.UserName = model.UserName;
        user.FirstName = model.FirstName;
        user.SurName = model.SurName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;

        await _userManager.UpdateAsync(user);
        return model;
    }

    /// <summary>
    /// Returns selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("{userId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> GetByIdUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Returns selected users role.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("role/{userId}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
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
    /// Change language selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("editcart")]
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
    /// Change language selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("setlanguage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetLanguage(string culture, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null && User.Identity.IsAuthenticated)
        {
            user.Language = culture;
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        return BadRequest();
    }

    /// <summary>
    /// Change role selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("changerole")]
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

    /// <summary>
    /// Delete selected user.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteUserAsync(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
        }

        return NoContent();
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