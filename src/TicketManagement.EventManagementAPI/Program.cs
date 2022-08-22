using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Serilog;
using TicketManagement.Common.JwtTokenAuth;
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
////services.AddHealthChecks().AddCheck<UserApiHealthcheck>("user_api_check", tags: new[] { "ready" });
services.AddOptions().Configure<UserApiOptions>(binder => binder.UserApiAddress = builder.Configuration["UserApiAddress"]);
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtAutheticationConstants.SchemeName)
    .AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>(JwtAutheticationConstants.SchemeName, null);

services.AddHttpClient<IUserClient, UserClient>((provider, client) =>
{
    var userApiAddress = provider.GetService<IOptions<UserApiOptions>>()?.Value.UserApiAddress;
    client.BaseAddress = new Uri(userApiAddress ?? string.Empty);
});

services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
services.AddControllers();
////services.AddSwaggerGen(options =>
////{
////    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Internal lab Demo 2", Version = "v1" });
////    options.IncludeXmlComments(XmlCommentsFilePathLazy.Value);
////    var jwtSecurityScheme = new OpenApiSecurityScheme
////    {
////        Description = "Jwt Token is required to access the endpoints",
////        In = ParameterLocation.Header,
////        Name = "JWT Authentication",
////        Type = SecuritySchemeType.Http,
////        Scheme = "bearer",
////        BearerFormat = "JWT",
////        Reference = new OpenApiReference
////        {
////            Id = JwtBearerDefaults.AuthenticationScheme,
////            Type = ReferenceType.SecurityScheme,
////        },
////    };

////    options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
////    options.AddSecurityRequirement(new OpenApiSecurityRequirement
////    {
////        { jwtSecurityScheme, Array.Empty<string>() },
////    });
////});

services.AddOpenApiDocument();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
////app.UseSwagger();
////app.UseSwaggerUI(options =>
////{
////    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer API v1");
////});

app.UseOpenApi();
app.UseSwaggerUi3();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    ////endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
    ////{
    ////    Predicate = check => check.Tags.Contains("ready"),
    ////}).WithMetadata(new AllowAnonymousAttribute());
    ////endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    ////{
    ////    Predicate = _ => false,
    ////}).WithMetadata(new AllowAnonymousAttribute());
});

app.Run();