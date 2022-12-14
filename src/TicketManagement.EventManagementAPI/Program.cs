using System.Net;
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

builder.WebHost.UseKestrel(options =>
{
    var configuration = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));
    var httpsPort = configuration.GetValue("ASPNETCORE_HTTPS_PORT", 5003);
    var certPassword = configuration.GetValue<string>("CertPassword");
    var certPath = configuration.GetValue<string>("CertPath");

    options.Listen(IPAddress.Any, httpsPort, listenOptions =>
    {
        listenOptions.UseHttps(certPath, certPassword);
    });
});

services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("https://localhost:7114", "https://localhost:7115");
    });
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
app.UseCors("CORSPolicy");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();