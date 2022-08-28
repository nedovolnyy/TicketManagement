using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the eventArea entity.
/// </summary>
[ApiController]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class EventAreaManagementController : ControllerBase
{
    private readonly IEventAreaRepository _eventAreaRepository;

    public EventAreaManagementController(IEventAreaRepository eventAreaRepository)
    {
        _eventAreaRepository = eventAreaRepository;
    }

    /// <summary>
    /// Returns list of the eventAreas.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("eventAreas")]
    [AllowAnonymous]
    public async Task<List<EventArea>> GetAllEventAreasAsync()
        => await _eventAreaRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new eventArea.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("eventArea")]
    public async Task InsertEventAreaAsync(EventArea eventArea)
    {
        ValidateAsync(eventArea);
        await _eventAreaRepository.InsertAsync(eventArea);
    }

    /// <summary>
    /// Update selected eventArea.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("eventArea")]
    public async Task UpdateEventAreaAsync(EventArea eventArea)
    {
        ValidateAsync(eventArea);
        await _eventAreaRepository.UpdateAsync(eventArea);
    }

    /// <summary>
    /// Delete selected eventArea.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("eventArea/{eventAreaId:int}")]
    public async Task DeleteEventAreaAsync(int eventAreaId)
    {
        await _eventAreaRepository.DeleteAsync(eventAreaId);
    }

    /// <summary>
    /// Returns selected eventArea.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("eventArea/{eventAreaId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventArea), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdEventAreaAsync(int eventAreaId)
    {
        var eventArea = await _eventAreaRepository.GetByIdAsync(eventAreaId);
        IActionResult result = eventArea is null ? NotFound() : Ok(eventArea);
        return result;
    }

    /// <summary>
    /// Returns list of the eventAreas into selected eventId.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("EventAreasByEventId/{eventId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventArea>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<EventArea>>> GetAllEventAreasByEventIdAsync(int eventId)
    {
        return await _eventAreaRepository.GetAllByEventId(eventId).ToListAsyncSafe();
    }

    internal void ValidateAsync(EventArea entity)
    {
        if (entity.EventId == default)
        {
            throw new ValidationException("The field 'EventId' of EventArea is not allowed to be null!");
        }

        if (entity.CoordX == default)
        {
            throw new ValidationException("The field 'CoordX' of EventArea is not allowed to be null!");
        }

        if (entity.CoordY == default)
        {
            throw new ValidationException("The field 'CoordY' of EventArea is not allowed to be null!");
        }

        if (entity.Price == default)
        {
            throw new ValidationException("The field 'Price' of EventArea is not allowed to be null!");
        }

        if (string.IsNullOrEmpty(entity.Description))
        {
            throw new ValidationException("The field 'Description' of EventArea is not allowed to be empty!");
        }
    }
}