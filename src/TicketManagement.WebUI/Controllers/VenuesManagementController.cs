using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.Identity;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = nameof(Roles.Administrator))]
public class VenuesManagementController : Controller
{
    private readonly VenueManagementApiClient _venueManagementApiClient;

    public VenuesManagementController(VenueManagementApiClient venueManagementApiClient)
    {
        _venueManagementApiClient = venueManagementApiClient;
    }

    public async Task<IActionResult> Index()
        => View(await _venueManagementApiClient.GetAllVenuesAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Venue venue)
    {
        await _venueManagementApiClient.InsertVenueAsync(venue);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
        => View(await _venueManagementApiClient.GetByIdVenueAsync(int.Parse(id)));

    [HttpPost]
    public async Task<IActionResult> Edit(Venue venue)
    {
        await _venueManagementApiClient.UpdateVenueAsync(venue);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        await _venueManagementApiClient.DeleteVenueAsync(int.Parse(id));
        return RedirectToAction("Index");
    }
}