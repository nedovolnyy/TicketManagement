using System.Diagnostics;
using System.Security.Claims;
using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.WebUI.Models;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers;

public class EventController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EventAreaManagementApiClient _eventAreaManagementApiClient;
    private readonly EventSeatManagementApiClient _eventSeatManagementApiClient;
    private readonly UsersManagementApiClient _usersManagementApiClient;

    public EventController(
        ILogger<HomeController> logger,
        EventAreaManagementApiClient eventAreaManagementApiClient,
        EventSeatManagementApiClient eventSeatManagementApiClient,
        UsersManagementApiClient usersManagementApiClient)
    {
        _logger = logger;
        _eventAreaManagementApiClient = eventAreaManagementApiClient;
        _eventSeatManagementApiClient = eventSeatManagementApiClient;
        _usersManagementApiClient = usersManagementApiClient;
    }

    public async Task<IActionResult> Index(int eventId)
    {
        if (eventId != default)
        {
            IEnumerable<EventArea> eventAreas = await _eventAreaManagementApiClient.GetAllEventAreasByEventIdAsync(eventId);
            if (eventAreas != null)
            {
                return View(eventAreas);
            }

            return NotFound();
        }

        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(int eventSeatId, string returnUrl, string price)
    {
        var invariantPrice = decimal.Parse(price);
        var user = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (User.Identity.IsAuthenticated)
        {
            if (user.Balance < invariantPrice)
            {
                return RedirectToRoute(new { controller = "Home", action = "NoBalance" });
            }
            else
            {
                await _usersManagementApiClient.PurchaseAsync(eventSeatId, returnUrl, price, User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _eventSeatManagementApiClient.ChangeEventSeatStatusAsync(eventSeatId);
            }
        }

        return LocalRedirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}