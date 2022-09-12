using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the eventSeat entity.
/// </summary>
[ApiController]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class EventSeatManagementController : ControllerBase
{
    private readonly IEventSeatRepository _eventSeatRepository;

    public EventSeatManagementController(IEventSeatRepository eventSeatRepository)
    {
        _eventSeatRepository = eventSeatRepository;
    }

    /// <summary>
    /// Returns list of the eventSeats.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("eventSeats")]
    [AllowAnonymous]
    public async Task<List<EventSeat>> GetAllEventSeatsAsync()
        => await _eventSeatRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new eventSeat.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("eventSeat")]
    public async Task InsertEventSeatAsync(EventSeat eventSeat)
    {
        ValidateAsync(eventSeat);
        await _eventSeatRepository.InsertAsync(eventSeat);
    }

    /// <summary>
    /// Update selected eventSeat.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("eventSeat")]
    public async Task UpdateEventSeatAsync(EventSeat eventSeat)
    {
        ValidateAsync(eventSeat);
        await _eventSeatRepository.UpdateAsync(eventSeat);
    }

    /// <summary>
    /// Delete selected eventSeat.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("eventSeat/{eventSeatId:int}")]
    public async Task DeleteEventSeatAsync(int eventSeatId)
    {
        await _eventSeatRepository.DeleteAsync(eventSeatId);
    }

    /// <summary>
    /// Returns selected eventSeat.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("eventSeat/{eventSeatId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventSeat), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdEventSeatAsync(int eventSeatId)
    {
        var eventSeat = await _eventSeatRepository.GetByIdAsync(eventSeatId);
        IActionResult result = eventSeat is null ? NotFound() : Ok(eventSeat);
        return result;
    }

    /// <summary>
    /// Change eventseat status by eventSeatId.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("EventSeatStatus/{eventSeatId:int}")]
    [AllowAnonymous]
    public async Task ChangeEventSeatStatusAsync(int eventSeatId)
    {
        await _eventSeatRepository.ChangeEventSeatStatusAsync(eventSeatId);
    }

    /// <summary>
    /// Returns list of the eventSeats into selected eventAreaId.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("EventSeatsByEventAreaId/{eventAreaId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventSeat>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<EventSeat>>> GetAllEventSeatsByEventAreaIdAsync(int eventAreaId)
    {
        return await _eventSeatRepository.GetAllByEventAreaId(eventAreaId).ToListAsyncSafe();
    }

    internal void ValidateAsync(EventSeat entity)
    {
        if (entity.EventAreaId == default)
        {
            throw new ValidationException("The field 'EventAreaId' of EventSeat is not allowed to be null!");
        }

        if (entity.Row == default)
        {
            throw new ValidationException("The field 'Row' of EventSeat is not allowed to be null!");
        }

        if (entity.Number == default)
        {
            throw new ValidationException("The field 'Number' of EventSeat is not allowed to be null!");
        }
    }
}