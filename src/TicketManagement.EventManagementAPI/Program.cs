using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Serilog;
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

services.AddEndpointsApiExplorer();
services.AddOptions().Configure<UserApiOptions>(binder => binder.UserApiAddress = builder.Configuration["UserApiAddress"]);
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, null);
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