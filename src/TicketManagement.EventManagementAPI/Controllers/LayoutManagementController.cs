using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;
using TicketManagement.EventManagementAPI.Helper;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the layout entity.
/// </summary>
[ApiController]
[AllowCrossSiteJson]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class LayoutManagementController : ControllerBase
{
    private readonly ILayoutRepository _layoutRepository;

    public LayoutManagementController(ILayoutRepository layoutRepository)
    {
        _layoutRepository = layoutRepository;
    }

    /// <summary>
    /// Returns list of the layouts.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("layouts")]
    [AllowAnonymous]
    public async Task<List<Layout>> GetAllLayoutsAsync()
        => await _layoutRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new layout.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("layout")]
    public async Task InsertLayoutAsync(Layout layout)
    {
        await ValidateAsync(layout);
        await _layoutRepository.InsertAsync(layout);
    }

    /// <summary>
    /// Update selected layout.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("layout")]
    public async Task UpdateLayoutAsync(Layout layout)
    {
        await ValidateAsync(layout);
        await _layoutRepository.UpdateAsync(layout);
    }

    /// <summary>
    /// Delete selected layout.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("layout/{layoutId:int}")]
    public async Task DeleteLayoutAsync(int layoutId)
    {
        await _layoutRepository.DeleteAsync(layoutId);
    }

    /// <summary>
    /// Returns selected layout.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("layout/{layoutId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Layout), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdLayoutAsync(int layoutId)
    {
        var layout = await _layoutRepository.GetByIdAsync(layoutId);
        IActionResult result = layout is null ? NotFound() : Ok(layout);
        return result;
    }

    /// <summary>
    /// Returns list of the layouts into selected layoutId.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("LayoutsByVenueId/{venueId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<Layout>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Layout>>> GetAllLayoutsByVenueIdAsync(int venueId)
    {
        return await _layoutRepository.GetAllByVenueId(venueId).ToListAsyncSafe();
    }

    internal async Task ValidateAsync(Layout entity)
    {
        if (entity.VenueId == default)
        {
            throw new ValidationException("The field 'VenueId' of Layout is not allowed to be null!");
        }

        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new ValidationException("The field 'Name' of Layout is not allowed to be empty!");
        }

        if (string.IsNullOrEmpty(entity.Description))
        {
            throw new ValidationException("The field 'Description' of Layout is not allowed to be empty!");
        }

        var layoutArray = await _layoutRepository.GetAllByVenueId(entity.VenueId).ToListAsyncSafe();
        foreach (var layout in layoutArray)
        {
            if (entity.Name == layout.Name)
            {
                throw new ValidationException("Layout name should be unique in venue!");
            }
        }
    }
}