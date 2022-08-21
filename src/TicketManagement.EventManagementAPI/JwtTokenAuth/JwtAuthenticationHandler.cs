using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TicketManagement.EventManagementAPI.Client;
using UserApiClientGenerated;

namespace TicketManagement.EventManagementAPI.JwtTokenAuth
{
    public class JwtAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
    {
        private readonly IUserClient _userClient;

        public JwtAuthenticationHandler(
            IOptionsMonitor<JwtAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserClient userClient)
            : base(options, logger, encoder, clock)
        {
            _userClient = userClient;
        }

        /// <inheritdoc />
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                await _userClient.ValidateToken(token);
            }
            catch
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
