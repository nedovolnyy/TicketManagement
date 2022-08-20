using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicketManagement.UserAPI.Settings;

namespace TicketManagement.UserAPI.Services
{
    public class JwtTokenService
    {
        private readonly JwtTokenSettings _settings;

        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            _settings = options.Value;
        }

        public string GetToken(IdentityUser user, IList<string> roles)
        {
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };
            userClaims.AddRange(roleClaims);
            var jwt = new JwtSecurityToken(
                issuer: _settings.JwtIssuer,
                audience: _settings.JwtAudience,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                    SecurityAlgorithms.HmacSha256),
                claims: userClaims);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string GenerateJwtToken(IdentityUser user, string roleName)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.JwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, roleName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
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
                    ValidateIssuer = true,
                    ValidIssuer = _settings.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = _settings.JwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                    ValidateLifetime = false,
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
