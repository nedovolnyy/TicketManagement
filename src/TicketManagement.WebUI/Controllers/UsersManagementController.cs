using Microsoft.AspNetCore.Mvc;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers;

////[Authorize(Roles = "Administrator")]
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

    public IActionResult Index() => View(_usersManagementApiClient.GetAllUsersAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateUser model)
    {
        await _usersManagementApiClient.CreateUserAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
        => View(await _usersManagementApiClient.GetByIdUserAsync(id));

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