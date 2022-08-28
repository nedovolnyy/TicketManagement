using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;

namespace TicketManagement.EventManagementAPI.Controllers;

/// <summary>
/// Resource for the operations against the area entity.
/// </summary>
[ApiController]
[Authorize(Roles = nameof(Roles.Administrator) + "," + nameof(Roles.EventManager))]
[Route("api/[controller]")]
[Produces("application/json")]
public class ThirdPartyEventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public ThirdPartyEventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Add new event from ThirdPartyEvent.
    /// </summary>
    /// <returns>.</returns>
    [HttpPost("ThirdPartyEvent")]
    public async Task InsertEventAsync(EventFromJson eventFromJson)
    {
        var imgBytes = Convert.FromBase64String(eventFromJson.EventLogoImage[(eventFromJson.EventLogoImage.LastIndexOf(',') + 1)..]);

        using var imageFile = new FileStream(eventFromJson.FullImagePath, FileMode.Create);
        imageFile.Write(imgBytes, 0, imgBytes.Length);
        imageFile.Flush();

        await _eventRepository.InsertAsync(eventFromJson.Event, eventFromJson.Price);
    }
}