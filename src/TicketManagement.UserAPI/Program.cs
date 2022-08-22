using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TicketManagement.Common.Identity;
using TicketManagement.Common.JwtTokenAuth.Services;
using TicketManagement.Common.JwtTokenAuth.Settings;
using TicketManagement.UserAPI.DataAccess;

var roles = new string[] { "Administrator", "EventManager" };

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.File(builder.Configuration["Logging:FilePath"], rollingInterval: RollingInterval.Day)
                        .WriteTo.Console()
                        .CreateLogger();
builder.Host.UseSerilog(logger);

builder.WebHost.UseUrls("https://*:5000").ConfigureKestrel(options =>
{
    options.ListenAnyIP(5004, configure => configure.UseHttps());
});

services.AddDbContext<UserApiDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>()
                    .AddRoles<Role>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<UserApiDbContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

var tokenSettings = builder.Configuration.GetSection(nameof(JwtTokenSettings));
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenSettings[nameof(JwtTokenSettings.JwtIssuer)],
            ValidateAudience = true,
            ValidAudience = tokenSettings[nameof(JwtTokenSettings.JwtAudience)],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings[nameof(JwtTokenSettings.JwtSecretKey)])),
            ValidateLifetime = false,
            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType,
        };
        options.SaveToken = true;
    });
services.AddAuthorization(options =>
{
    foreach (var prop in roles)
    {
        options.AddPolicy(prop, policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, roles));
    }
});

services.Configure<JwtTokenSettings>(tokenSettings);
services.AddScoped<JwtTokenService>();

services.AddControllers();

services.AddOpenApiDocument();

var app = builder.Build();

JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

app.UseSerilogRequestLogging();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseOpenApi();
app.UseSwaggerUi3();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();