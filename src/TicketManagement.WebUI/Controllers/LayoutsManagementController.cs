using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = nameof(Roles.Administrator))]
public class LayoutsManagementController : Controller
{
    private readonly IServiceProvider _serviceProvider;

    public LayoutsManagementController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IActionResult> Index()
        => View(await _serviceProvider.GetRequiredService<ILayoutService>().GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Layout layout)
    {
        await _serviceProvider.GetRequiredService<ILayoutService>().InsertAsync(layout);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(string id)
        => View(await _serviceProvider.GetRequiredService<ILayoutService>().GetByIdAsync(int.Parse(id)));

    [HttpPost]
    public async Task<IActionResult> Edit(Layout layout)
    {
        await _serviceProvider.GetRequiredService<ILayoutService>().UpdateAsync(layout);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        await _serviceProvider.GetRequiredService<ILayoutService>().DeleteAsync(int.Parse(id));
        return RedirectToAction("Index");
    }
}