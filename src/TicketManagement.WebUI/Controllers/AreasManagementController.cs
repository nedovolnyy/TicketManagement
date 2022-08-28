using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.Identity;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = nameof(Roles.Administrator))]
public class AreasManagementController : Controller
{
    private readonly AreaManagementApiClient _areaManagementApiClient;

    public AreasManagementController(AreaManagementApiClient areaManagementApiClient)
    {
        _areaManagementApiClient = areaManagementApiClient;
    }

    public async Task<IActionResult> Index()
        => View(await _areaManagementApiClient.GetAllAreasAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Area area)
    {
        await _areaManagementApiClient.InsertAreaAsync(area);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
        => View(await _areaManagementApiClient.GetByIdAreaAsync(int.Parse(id)));

    [HttpPost]
    public async Task<IActionResult> Edit(Area area)
    {
        await _areaManagementApiClient.UpdateAreaAsync(area);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        await _areaManagementApiClient.DeleteAreaAsync(int.Parse(id));
        return RedirectToAction("Index");
    }
}