using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;
using TicketManagement.EventManagementAPI.Helper;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the seat entity.
/// </summary>
[ApiController]
[AllowCrossSiteJson]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class SeatManagementController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;

    public SeatManagementController(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    /// <summary>
    /// Returns list of the seats.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("seats")]
    [AllowAnonymous]
    public async Task<List<Seat>> GetAllSeatsAsync()
        => await _seatRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new seat.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("seat")]
    public async Task InsertSeatAsync(Seat seat)
    {
        await ValidateAsync(seat);
        await _seatRepository.InsertAsync(seat);
    }

    /// <summary>
    /// Update selected seat.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("seat")]
    public async Task UpdateSeatAsync(Seat seat)
    {
        await ValidateAsync(seat);
        await _seatRepository.UpdateAsync(seat);
    }

    /// <summary>
    /// Delete selected seat.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("seat/{seatId:int}")]
    public async Task DeleteSeatAsync(int seatId)
    {
        await _seatRepository.DeleteAsync(seatId);
    }

    /// <summary>
    /// Returns selected seat.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("seat/{seatId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Seat), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdSeatAsync(int seatId)
    {
        var seat = await _seatRepository.GetByIdAsync(seatId);
        IActionResult result = seat is null ? NotFound() : Ok(seat);
        return result;
    }

    /// <summary>
    /// Returns list of the seats into selected areaId.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("SeatsByAreaId/{areaId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<Seat>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Seat>>> GetAllSeatsByAreaIdAsync(int areaId)
    {
        return await _seatRepository.GetAllByAreaId(areaId).ToListAsyncSafe();
    }

    internal async Task ValidateAsync(Seat entity)
    {
        if (entity.AreaId == default)
        {
            throw new ValidationException("The field 'AreaId' of Seat is not allowed to be null!");
        }

        if (entity.Row == default)
        {
            throw new ValidationException("The field 'Row' of Seat is not allowed to be null!");
        }

        if (entity.Number == default)
        {
            throw new ValidationException("The field 'Number' of Seat is not allowed to be null!");
        }

        var seatArray = await _seatRepository.GetAllByAreaId(entity.AreaId).ToListAsyncSafe();
        foreach (var seat in seatArray)
        {
            if (entity.Row == seat.Row && entity.Number == seat.Number)
            {
                throw new ValidationException("Row and number should be unique for area!");
            }
        }
    }
}