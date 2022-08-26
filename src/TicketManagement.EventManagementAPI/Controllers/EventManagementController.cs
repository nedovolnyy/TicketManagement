using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the event entity.
/// </summary>
[ApiController]
[Authorize(Roles = nameof(Roles.Administrator) + "," + nameof(Roles.EventManager))]
[Route("api/[controller]")]
[Produces("application/json")]
public class EventManagementController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventManagementController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Returns list of the events.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("events")]
    [AllowAnonymous]
    public async Task<List<Event>> GetAllEventsAsync()
        => await _eventRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new event.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("event")]
    public async Task InsertEventAsync(Event @event, [Optional] decimal price)
    {
        await ValidateAsync(@event);
        await _eventRepository.InsertAsync(@event, price);
    }

    /// <summary>
    /// Update selected event.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("event")]
    public async Task UpdateEventAsync(Event @event, [Optional] decimal price)
    {
        await ValidateAsync(@event);
        await _eventRepository.UpdateAsync(@event, price);
    }

    /// <summary>
    /// Delete selected event.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("event/{eventId:int}")]
    public async Task DeleteEventAsync(int eventId)
    {
        await _eventRepository.DeleteAsync(eventId);
    }

    /// <summary>
    /// Returns selected event.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("event/{eventId:int}")]
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
    [HttpGet("EventsByLayoutId/{layoutId:int}")]
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
    [HttpGet("PriceByEventId/{eventId:int}")]
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
    [HttpGet("SeatsAvailableCount/{eventId:int}")]
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
    [HttpGet("SeatsCount/{layoutId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<int> GetSeatsCountAsync(int layoutId)
    {
        return await _eventRepository.GetSeatsCountAsync(layoutId);
    }

    private async Task EventValidate(Event entity)
    {
        if ((entity.EventTime.Ticks - DateTimeOffset.Now.Ticks) < 0)
        {
            throw new ValidationException("Event can't be created in the past!");
        }

        if (entity.EventTime > entity.EventEndTime)
        {
            throw new ValidationException("EventEndTime cannot be later than EventTime!");
        }

        if (entity.Id == default)
        {
            var evntArray = await _eventRepository.GetAllByLayoutId(entity.LayoutId).ToListAsyncSafe();
            foreach (var evnt in evntArray)
            {
                if (entity.LayoutId == evnt.LayoutId && entity.Name == evnt.Name)
                {
                    throw new ValidationException("Layout name should be unique in venue!");
                }

                if (entity.LayoutId == evnt.LayoutId && entity.EventTime == evnt.EventTime)
                {
                    throw new ValidationException("Do not create event for the same layout in the same time!");
                }
            }
        }

        if (await GetSeatsCountAsync(entity.LayoutId) == default)
        {
            throw new ValidationException("Create event is not possible! Haven't seats in Area!");
        }
    }

    internal async Task ValidateAsync(Event entity)
    {
        if (entity.LayoutId == default)
        {
            throw new ValidationException("The field 'LayoutId' of Event is not allowed to be null!");
        }

        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new ValidationException("The field 'Name' of Event is not allowed to be empty!");
        }

        if (string.IsNullOrEmpty(entity.Description))
        {
            throw new ValidationException("The field 'Description' of Event is not allowed to be empty!");
        }

        if (string.IsNullOrEmpty(entity.EventLogoImage))
        {
            throw new ValidationException("The field 'EventLogoImage' of Event is not allowed to be empty!");
        }

        await EventValidate(entity);
    }
}