using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;
using TicketManagement.WebUI.Services;

namespace TicketManagement.WebUI.Controllers;

[Authorize(Roles = nameof(Roles.Administrator) + "," + nameof(Roles.EventManager))]
public class ThirdPartyEventsController : Controller
{
    private readonly ListThirdPartyEventsService _listThirdPartyEventsService;
    private readonly IThirdPartyEventService _thirdPartyEventService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ThirdPartyEventsController(ListThirdPartyEventsService listThirdPartyEventsService, IThirdPartyEventService thirdPartyEventService, IWebHostEnvironment webHostEnvironment)
    {
        _listThirdPartyEventsService = listThirdPartyEventsService;
        _thirdPartyEventService = thirdPartyEventService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<ActionResult> Add(Event @event, decimal thirdPartyEventPrice)
    {
        var shortImagePath = "image" + Path.DirectorySeparatorChar + @event.Name + @event.LayoutId + ".png";
        var fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, shortImagePath);

        await _thirdPartyEventService.InsertAsync(new EventFromJson
        {
            Event = @event,
            EventLogoImage = @event.EventLogoImage,
            FullImagePath = fullImagePath,
            Price = thirdPartyEventPrice,
        });

        var thirdPartyEvents = _listThirdPartyEventsService.Add(@event, thirdPartyEventPrice);
        return View("Preview", thirdPartyEvents);
    }

    [HttpPost]
    public ActionResult Delete(string thirdPartyEventName, string thirdPartyEventDescription, DateTimeOffset thirdPartyEventTime)
    {
        var thirdPartyEvents = _listThirdPartyEventsService.Delete(thirdPartyEventName, thirdPartyEventDescription, thirdPartyEventTime);
        return View("Preview", thirdPartyEvents);
    }

    [HttpPost]
    public async Task<ActionResult> Preview(IFormFile file)
    {
        var thirdPartyEvents = await _listThirdPartyEventsService.PreviewAsync(file);
        return View(thirdPartyEvents);
    }
}
