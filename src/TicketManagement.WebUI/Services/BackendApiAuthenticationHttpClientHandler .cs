using System.Net.Http.Headers;

namespace TicketManagement.WebUI.Services;

public sealed class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}