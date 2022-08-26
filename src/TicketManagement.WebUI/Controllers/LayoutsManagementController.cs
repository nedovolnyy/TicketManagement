using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.Identity;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = nameof(Roles.Administrator))]
public class LayoutsManagementController : Controller
{
    private readonly LayoutManagementApiClient _layoutManagementApiClient;

    public LayoutsManagementController(LayoutManagementApiClient layoutManagementApiClient)
    {
        _layoutManagementApiClient = layoutManagementApiClient;
    }

    public async Task<IActionResult> Index()
        => View(await _layoutManagementApiClient.GetAllLayoutsAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Layout layout)
    {
        await _layoutManagementApiClient.InsertLayoutAsync(layout);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
        => View(await _layoutManagementApiClient.GetByIdLayoutAsync(int.Parse(id)));

    [HttpPost]
    public async Task<IActionResult> Edit(Layout layout)
    {
        await _layoutManagementApiClient.UpdateLayoutAsync(layout);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        await _layoutManagementApiClient.DeleteLayoutAsync(int.Parse(id));
        return RedirectToAction("Index");
    }
}