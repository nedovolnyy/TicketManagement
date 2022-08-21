using System.Diagnostics;
using System.Security.Claims;
using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.WebUI.Models;
using TicketManagement.WebUI.Services;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers;

[SerilogMvcLogging]
public class HomeController : Controller
{
    private readonly EventManagementApiClient _eventManagementApiClient;
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public HomeController(EventManagementApiClient eventManagementApiClient, UsersManagementApiClient usersManagementApiClient)
    {
        _eventManagementApiClient = eventManagementApiClient;
        _usersManagementApiClient = usersManagementApiClient;
    }

    public async Task<IActionResult> IndexAsync()
    {
        return View(await _eventManagementApiClient.GetAllEventsAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Event @event)
    {
        await _eventManagementApiClient.InsertEventAsync(decimal.Zero, @event);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Cart()
    {
        var tempUser = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(tempUser);
    }

    [HttpPost]
    public async Task<IActionResult> Cart(string money)
    {
        await _usersManagementApiClient.EditCartAsync(money, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return RedirectToAction("Cart");
    }

    public async Task<IActionResult> NoBalance()
    {
        return View(await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
    {
        await _usersManagementApiClient.SetLanguageAsync(culture, User.FindFirstValue(ClaimTypes.NameIdentifier));

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

        return LocalRedirect(returnUrl);
    }
}