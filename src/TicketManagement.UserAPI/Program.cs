using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Serilog;
using TicketManagement.Common;
using TicketManagement.Common.Identity;
using TicketManagement.UserAPI.DataAccess;
using TicketManagement.UserAPI.Services;

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

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Settings.Jwt.JwtIssuer,
            ValidateAudience = true,
            ValidAudience = Settings.Jwt.JwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Jwt.JwtSecretKey)),
            ValidateLifetime = false,
            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Authorization"];
                return Task.CompletedTask;
            },
        };
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
    })
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Identity/Account/Login/";
    });

services.AddAuthorization(options =>
{
    foreach (var prop in Enum.GetValues<Roles>())
    {
        options.AddPolicy(prop.GetDisplayName(), policy =>
        {
            policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, Enum.GetNames<Roles>());
        });
    }
});

services.AddControllers();
services.AddScoped<JwtTokenService>();
services.AddOpenApiDocument();

var app = builder.Build();

JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});
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
