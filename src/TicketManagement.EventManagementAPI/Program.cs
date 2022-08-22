using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TicketManagement.Common.JwtTokenAuth;
using TicketManagement.Common.JwtTokenAuth.Settings;
using TicketManagement.EventManagementAPI;
using TicketManagement.EventManagementAPI.Client;
using TicketManagement.EventManagementAPI.JwtTokenAuth;

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
    options.ListenAnyIP(5003, configure => configure.UseHttps());
});

var tokenSettings = builder.Configuration.GetSection(nameof(JwtTokenSettings));

services.AddEndpointsApiExplorer();
services.AddOptions().Configure<UserApiOptions>(binder => binder.UserApiAddress = builder.Configuration["UserApiAddress"]);
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddAuthentication()
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

services.AddHttpClient<IUserClient, UserClient>((provider, client) =>
{
    var userApiAddress = provider.GetService<IOptions<UserApiOptions>>()?.Value.UserApiAddress;
    client.BaseAddress = new Uri(userApiAddress ?? string.Empty);
});

services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
services.AddControllers();

services.AddOpenApiDocument();

var app = builder.Build();

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