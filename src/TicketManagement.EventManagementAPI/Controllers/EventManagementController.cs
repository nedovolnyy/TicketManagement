using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.Entities;
using TicketManagement.EventManagementAPI.Helper;

namespace TicketManagement.EventManagementAPI.Controllers
{
    /// <summary>
    /// Resource for the operations against the event entity.
    /// </summary>
    [ApiController]
    [Authorize(Roles = "EventManager,Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EventsManagementController : ControllerBase
    {
        private IEnumerable<EventWithPrice> _eventWithPrices = new List<EventWithPrice>();

        public EventsManagementController(IServiceProvider servicesProvider)
        {
            EventRepositoryResolver.Configure(servicesProvider);
        }

        /// <summary>
        /// Returns list of the events.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<EventWithPrice>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEvents()
        {
            _eventWithPrices = await _eventWithPrices.GetAllAsync();
            IActionResult result = _eventWithPrices is null ? NotFound() : Ok(_eventWithPrices);
            return result;
        }

        /// <summary>
        /// Add new event.
        /// </summary>
        /// <returns>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertAsync(EventWithPrice eventWithPrice)
        {
            await _eventWithPrices.InsertAsync(eventWithPrice);
            return Ok();
        }

        /// <summary>
        /// Returns selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("{eventId:int}")]
        [ProducesResponseType(typeof(EventWithPrice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int eventId)
        {
            var eventWithPrice = await _eventWithPrices.GetByIdAsync(eventId);
            IActionResult result = eventWithPrice is null ? NotFound() : Ok(eventWithPrice);
            return result;
        }

        /// <summary>
        /// Update selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpPut]
        public async Task<IActionResult> Edit(EventWithPrice eventWithPrice)
        {
            await _eventWithPrices.UpdateAsync(eventWithPrice);
            return Ok();
        }

        /// <summary>
        /// Delete selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpDelete("{eventId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int eventId)
        {
            await _eventWithPrices.DeleteAsync(eventId);
            return NoContent();
        }
    }
}