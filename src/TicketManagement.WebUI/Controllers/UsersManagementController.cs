using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class UsersManagementController : Controller
{
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public UsersManagementController(UsersManagementApiClient usersManagementApiClient)
    {
        _usersManagementApiClient = usersManagementApiClient;
    }

    public string ReturnUrl { get; set; }

    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> Index() => View(await _usersManagementApiClient.GetAllUsersAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateUser model)
    {
        await _usersManagementApiClient.CreateUserAsync(model.Password, new User { Email = model.Email, FirstName = model.FirstName, SurName = model.SurName, });
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _usersManagementApiClient.GetByIdUserAsync(id);
        return View(new CreateUser
        {
            Email = user.Email,
            FirstName = user.FirstName,
            SurName = user.SurName,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CreateUser model)
        => View(await _usersManagementApiClient.EditUserAsync(model));

    public async Task<IActionResult> ChangeRole(string userId)
        => View(await _usersManagementApiClient.GetRoleByIdAsync(userId));

    [HttpPost]
    public async Task<IActionResult> ChangeRole(string userId, IEnumerable<string> roles)
    {
        await _usersManagementApiClient.ChangeRoleAsync(userId, roles);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await _usersManagementApiClient.DeleteUserAsync(id);
        return RedirectToAction("Index");
    }
}