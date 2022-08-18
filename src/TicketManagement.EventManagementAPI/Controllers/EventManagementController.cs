using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.EventManagementAPI.Controllers
{
    /// <summary>
    /// Resource for the operations against the event entity.
    /// </summary>
    [ApiController]
    ////[Authorize(Roles = "EventManager,Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EventsManagementController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsManagementController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Returns list of the events.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAll().ToListAsyncSafe();
            IActionResult result = events is null ? NotFound() : Ok(events);
            return result;
        }

        /// <summary>
        /// Add new event.
        /// </summary>
        /// <returns>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertEventAsync(Event @event, decimal price)
        {
            await _eventRepository.InsertAsync(@event, price);
            return Ok();
        }

        /// <summary>
        /// Returns selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("{eventId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdEventAsync(int eventId)
        {
            var @event = await _eventRepository.GetByIdAsync(eventId);
            IActionResult result = @event is null ? NotFound() : Ok(@event);
            return result;
        }

        /// <summary>
        /// Update selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync(Event @event, decimal price)
        {
            await _eventRepository.UpdateAsync(@event, price);
            return Ok();
        }

        /// <summary>
        /// Delete selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpDelete("{eventId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEventAsync(int eventId)
        {
            await _eventRepository.DeleteAsync(eventId);
            return NoContent();
        }

        /// <summary>
        /// Is all available seats in selected event.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("IsAllAvailableSeats/{eventId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> IsAllAvailableSeatsAsync(int eventId)
        {
            return await _eventRepository.IsAllAvailableSeatsAsync(eventId);
        }

        /// <summary>
        /// Returns list of the events into selected layoutId.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("GetAllEventsByLayoutId/{layoutId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEventsByLayoutIdAsync(int layoutId)
        {
            return await _eventRepository.GetAllByLayoutId(layoutId).ToListAsyncSafe();
        }

        /// <summary>
        /// Get price by eventId.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("GetPriceByEventId/{eventId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetPriceByEventIdAsync(int eventId)
        {
            return await _eventRepository.GetPriceByEventIdAsync(eventId);
        }

        /// <summary>
        /// Get seats available count by eventId..
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("GetSeatsAvailableCount/{eventId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetSeatsAvailableCountAsync(int eventId)
        {
            return await _eventRepository.GetSeatsAvailableCountAsync(eventId);
        }

        /// <summary>
        /// Get seats count by layoutId.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet("GetSeatsCount/{layoutId:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetSeatsCountAsync(int layoutId)
        {
            return await _eventRepository.GetSeatsCountAsync(layoutId);
        }
    }
}