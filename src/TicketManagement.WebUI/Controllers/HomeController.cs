using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketManagement.WebUI.Models;
using TicketManagement.WebUI.Services;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Controllers;

[SerilogMvcLogging]
public class HomeController : Controller
{
    private readonly EventManagementApiClient _eventManagementApiClient;
    private readonly UsersManagementApiClient _usersManagementApiClient;
    private readonly HttpClient _client;
    private readonly string _url = string.Format("https://localhost:5004/api/UsersManagement");
#pragma warning disable S103 // Lines should not be too long
    private readonly string _bearerToken = string.Format("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE1MDlmZDcwLWMyMzQtNDZmYy1iZGFkLTFkNGVkNTRlNGNiNCIsInN1YiI6IkFETUlOQEFETUlOLkNPTSIsImVtYWlsIjoiQURNSU5AQURNSU4uQ09NIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsIm5iZiI6MTY2MTEyMTU3OCwiZXhwIjoxNjYxMTIyMTc4LCJpYXQiOjE2NjExMjE1NzgsImlzcyI6Ik15U2VjcmV0SXNzdWVyIiwiYXVkIjoiTXlTZWNyZXRBdWRpZW5jZSJ9.8GzNuoUXS8rDh3uaQJ3MB9GZx2wNFcUlpauW35OY9ewfIFnqi-fk_IK9DikZmSWgrZhzkj98BxDTrngO4To3nQ");
#pragma warning restore S103 // Lines should not be too long

    public HomeController(EventManagementApiClient eventManagementApiClient, UsersManagementApiClient usersManagementApiClient)
    {
        _eventManagementApiClient = eventManagementApiClient;
        _usersManagementApiClient = usersManagementApiClient;

        _client = new HttpClient();
        _client.BaseAddress = new Uri(_url);
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
    }

    public async Task<ActionResult> GV()
    {
        HttpResponseMessage responseMessage = await _client.GetAsync(_url);
        if (responseMessage.IsSuccessStatusCode)
        {
            var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            var jsonResponse = JsonConvert.DeserializeObject<List<string>>(responseData);
            return View(jsonResponse);
        }

        return View("Error");
    }

    public async Task<IActionResult> IndexAsync()
    {
        return View(await _eventManagementApiClient.GetAllEventsAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Event @event)
    {
        await _eventManagementApiClient.InsertEventAsync(decimal.Zero, @event);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CartAsync()
    {
        var tempUser = await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return View(tempUser);
    }

    [HttpPost]
    public async Task<IActionResult> Cart(string money)
    {
        await _usersManagementApiClient.EditCartAsync(money, User.FindFirstValue(ClaimTypes.NameIdentifier));
        return RedirectToAction("Cart");
    }

    public async Task<IActionResult> NoBalanceAsync()
    {
        return View(await _usersManagementApiClient.GetByIdUserAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
    {
        await _usersManagementApiClient.SetLanguageAsync(culture, User.FindFirstValue(ClaimTypes.NameIdentifier));

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

        return LocalRedirect(returnUrl);
    }
}