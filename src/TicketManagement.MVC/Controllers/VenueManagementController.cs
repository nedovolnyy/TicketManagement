using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VenueManagementController : Controller
    {
        private readonly IServiceProvider _serviceProvider;

        public VenueManagementController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
            => View(await _serviceProvider.GetRequiredService<IVenueService>().GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Venue venue)
        {
            await _serviceProvider.GetRequiredService<IVenueService>().InsertAsync(venue);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
            => View(await _serviceProvider.GetRequiredService<IVenueService>().GetByIdAsync(int.Parse(id)));

        [HttpPost]
        public async Task<IActionResult> Edit(Venue venue)
        {
            await _serviceProvider.GetRequiredService<IVenueService>().UpdateAsync(venue);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            await _serviceProvider.GetRequiredService<IVenueService>().DeleteAsync(int.Parse(id));
            return RedirectToAction("Index");
        }
    }
}