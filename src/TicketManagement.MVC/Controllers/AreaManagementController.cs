using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AreaManagementController : Controller
    {
        private readonly IServiceProvider _serviceProvider;

        public AreaManagementController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
            => View(await _serviceProvider.GetRequiredService<IAreaService>().GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Area area)
        {
            await _serviceProvider.GetRequiredService<IAreaService>().InsertAsync(area);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
            => View(await _serviceProvider.GetRequiredService<IAreaService>().GetByIdAsync(int.Parse(id)));

        [HttpPost]
        public async Task<IActionResult> Edit(Area area)
        {
            await _serviceProvider.GetRequiredService<IAreaService>().UpdateAsync(area);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            await _serviceProvider.GetRequiredService<IAreaService>().DeleteAsync(int.Parse(id));
            return RedirectToAction("Index");
        }
    }
}