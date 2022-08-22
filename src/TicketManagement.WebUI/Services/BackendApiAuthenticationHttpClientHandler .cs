using System.Net.Http.Headers;
using System.Security.Claims;
using TicketManagement.Common.Identity;
using TicketManagement.Common.JwtTokenAuth.Services;

namespace TicketManagement.WebUI.Services;

public sealed class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;
    private readonly JwtTokenService _jwtTokenService;
#pragma warning disable S103 // Lines should not be too long
#pragma warning disable S1144 // Unused private types or members should be removed
    private readonly string _bearerToken = string.Format("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE1MDlmZDcwLWMyMzQtNDZmYy1iZGFkLTFkNGVkNTRlNGNiNCIsInN1YiI6IkFETUlOQEFETUlOLkNPTSIsImVtYWlsIjoiQURNSU5AQURNSU4uQ09NIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsIm5iZiI6MTY2MTEyMTU3OCwiZXhwIjoxNjYxMTIyMTc4LCJpYXQiOjE2NjExMjE1NzgsImlzcyI6Ik15U2VjcmV0SXNzdWVyIiwiYXVkIjoiTXlTZWNyZXRBdWRpZW5jZSJ9.8GzNuoUXS8rDh3uaQJ3MB9GZx2wNFcUlpauW35OY9ewfIFnqi-fk_IK9DikZmSWgrZhzkj98BxDTrngO4To3nQ");
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S103 // Lines should not be too long

    public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor, JwtTokenService jwtTokenService)
    {
        _accessor = accessor;
        _jwtTokenService = jwtTokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_accessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var user = _accessor.HttpContext.User;

            var token = _jwtTokenService.GenerateJwtToken(new User
            {
                Id = user.FindFirst(ClaimTypes.NameIdentifier).Value,
                Email = user.FindFirst(ClaimTypes.Email).Value,
            }, user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList());

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}