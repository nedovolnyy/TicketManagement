using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the area entity.
/// </summary>
[ApiController]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class AreaManagementController : ControllerBase
{
    private readonly IAreaRepository _areaRepository;

    public AreaManagementController(IAreaRepository areaRepository)
    {
        _areaRepository = areaRepository;
    }

    /// <summary>
    /// Returns list of the areas.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("areas")]
    [AllowAnonymous]
    public async Task<List<Area>> GetAllAreasAsync()
        => await _areaRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new area.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("area")]
    public async Task InsertAreaAsync(Area area)
    {
        await ValidateAsync(area);
        await _areaRepository.InsertAsync(area);
    }

    /// <summary>
    /// Update selected area.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("area")]
    public async Task UpdateAreaAsync(Area area)
    {
        await ValidateAsync(area);
        await _areaRepository.UpdateAsync(area);
    }

    /// <summary>
    /// Delete selected area.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("area/{areaId:int}")]
    public async Task DeleteAreaAsync(int areaId)
    {
        await _areaRepository.DeleteAsync(areaId);
    }

    /// <summary>
    /// Returns selected area.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("area/{areaId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Area), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAreaAsync(int areaId)
    {
        var area = await _areaRepository.GetByIdAsync(areaId);
        IActionResult result = area is null ? NotFound() : Ok(area);
        return result;
    }

    /// <summary>
    /// Returns list of the areas into selected layoutId.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("AreasByLayoutId/{layoutId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<Area>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Area>>> GetAllAreasByLayoutIdAsync(int layoutId)
    {
        return await _areaRepository.GetAllByLayoutId(layoutId).ToListAsyncSafe();
    }

    internal async Task ValidateAsync(Area entity)
    {
        if (entity.LayoutId == default)
        {
            throw new ValidationException("The field 'LayoutId' of Area is not allowed to be null!");
        }

        if (entity.CoordX == default)
        {
            throw new ValidationException("The field 'CoordX' of Area is not allowed to be null!");
        }

        if (entity.CoordY == default)
        {
            throw new ValidationException("The field 'CoordY' of Area is not allowed to be null!");
        }

        if (string.IsNullOrEmpty(entity.Description))
        {
            throw new ValidationException("The field 'Description' of Area is not allowed to be empty!");
        }

        var areaArray = await _areaRepository.GetAllByLayoutId(entity.LayoutId).ToListAsyncSafe();
        foreach (var area in areaArray)
        {
            if (entity.Description == area.Description)
            {
                throw new ValidationException("Area description should be unique for area!");
            }
        }
    }
}