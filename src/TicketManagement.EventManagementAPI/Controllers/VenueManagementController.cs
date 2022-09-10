using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.Common.Validation;
using TicketManagement.EventManagementAPI.Helper;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the venue entity.
/// </summary>
[ApiController]
[AllowCrossSiteJson]
[Authorize(Roles = nameof(Roles.Administrator))]
[Route("api/[controller]")]
[Produces("application/json")]
public class VenueManagementController : ControllerBase
{
    private readonly IVenueRepository _venueRepository;

    public VenueManagementController(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    /// <summary>
    /// Returns list of the venues.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("venues")]
    [AllowAnonymous]
    public async Task<List<Venue>> GetAllVenuesAsync()
        => await _venueRepository.GetAll().ToListAsyncSafe();

    /// <summary>
    /// Add new venue.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("venue")]
    public async Task InsertVenueAsync(Venue venue)
    {
        await ValidateAsync(venue);
        await _venueRepository.InsertAsync(venue);
    }

    /// <summary>
    /// Update selected venue.
    /// </summary>
    /// <returns>.</returns>
    [HttpPut("venue")]
    public async Task UpdateVenueAsync(Venue venue)
    {
        await ValidateAsync(venue);
        await _venueRepository.UpdateAsync(venue);
    }

    /// <summary>
    /// Delete selected venue.
    /// </summary>
    /// <returns>.</returns>
    [HttpDelete("venue/{venueId:int}")]
    public async Task DeleteVenueAsync(int venueId)
    {
        await _venueRepository.DeleteAsync(venueId);
    }

    /// <summary>
    /// Returns selected venue.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("venue/{venueId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Venue), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdVenueAsync(int venueId)
    {
        var venue = await _venueRepository.GetByIdAsync(venueId);
        IActionResult result = venue is null ? NotFound() : Ok(venue);
        return result;
    }

    /// <summary>
    /// Return VenueId by venues name.
    /// </summary>
    /// <returns>.</returns>
    [HttpGet("VenueIdByName/{name}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetVenueIdByNameAsync(string name)
    {
        return await _venueRepository.GetIdFirstByNameAsync(name);
    }

    internal async Task ValidateAsync(Venue entity)
    {
        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new ValidationException("The field 'Name' of Venue is not allowed to be empty!");
        }

        if (string.IsNullOrEmpty(entity.Address))
        {
            throw new ValidationException("The field 'Address' of Venue is not allowed to be empty!");
        }

        if (string.IsNullOrEmpty(entity.Description))
        {
            throw new ValidationException("The field 'Description' of Venue is not allowed to be empty!");
        }

        var venueId = await _venueRepository.GetIdFirstByNameAsync(entity.Name);
        if (venueId != default)
        {
            throw new ValidationException("The Venue name is not unique!");
        }
    }
}