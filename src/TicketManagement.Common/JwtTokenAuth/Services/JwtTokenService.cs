using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketManagement.Common.JwtTokenAuth.Settings;

namespace TicketManagement.Common.JwtTokenAuth.Services
{
    public class JwtTokenService
    {
        private readonly JwtTokenSettings _settings;

        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateJwtToken(IdentityUser user, IEnumerable<string> roles)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            userClaims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Issuer = _settings.JwtIssuer,
                Audience = _settings.JwtAudience,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)), SecurityAlgorithms.HmacSha512Signature),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidIssuer = _settings.JwtIssuer,
                    ValidAudience = _settings.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    RoleClaimType = ClaimsIdentity.DefaultRoleClaimType,
                },
                out var _);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
