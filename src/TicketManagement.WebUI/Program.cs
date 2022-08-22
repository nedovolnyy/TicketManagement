using System.Globalization;
using System.Security.Claims;
using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketManagement.Common.JwtTokenAuth.Services;
using TicketManagement.WebUI;
using TicketManagement.WebUI.Services;
using UserApiClientGenerated;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.File(builder.Configuration["Logging:FilePath"], rollingInterval: RollingInterval.Day)
                        .WriteTo.Console()
                        .CreateLogger();
builder.Host.UseSerilog(logger);

services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("be-BY"),
                    new CultureInfo("ru-RU"),
                };
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.DefaultRequestCulture = new RequestCulture("en-US");
});

services.AddLocalization(options => options.ResourcesPath = "Resources");

services
    .AddControllersWithViews()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
    .AddViewLocalization();

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Identity/Account/Login/";
    });
////services.AddSwaggerGen(options =>
////{
////    options.SwaggerDoc("v1", new OpenApiInfo
////    {
////        Title = "Internal lab Demo 2",
////        Version = "v1",
////    });
////    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
////    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
////    options.IncludeXmlComments(xmlPath);
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
////                {
////                    { jwtSecurityScheme, Array.Empty<string>() },
////                });
////});

services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Administrator");
    });

    options.AddPolicy("EventManager", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "EventManager")
                                      || x.User.HasClaim(ClaimTypes.Role, "Administrator"));
    });

    options.AddPolicy("User", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "User");
    });
});

services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(60));
services.AddControllersWithViews(options =>
{
    options.Filters.Add<SerilogMvcLoggingAttribute>();
});

services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));

services.AddHttpClient("EventManagementAPIClient").ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(builder.Configuration["EventManagementApiAddress"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
services.AddScoped(scope =>
{
    var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementAPIClient");
    var baseUrl = builder.Configuration["EventManagementApiAddress"];
    return new EventManagementApiClient(baseUrl, httpClient);
});

services.AddTransient<BackendApiAuthenticationHttpClientHandler>();
services.AddHttpClient("UsersAPIClient").ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(builder.Configuration["UserApiAddress"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
services.AddScoped(scope =>
{
    var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("UsersAPIClient");
    var baseUrl = builder.Configuration["UserApiAddress"];
    return new UsersManagementApiClient(baseUrl, httpClient);
});

services.AddBLLServices();
services.AddSingleton<ListThirdPartyEventsService>();
services.AddSingleton<JwtTokenService>();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddMvc(options =>
{
    options.Filters.Add(new RequireHttpsAttribute());
    options.Filters.Add<SerilogMvcLoggingAttribute>();
});

services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 5004));
////app.UseIdentityServer();
app.UseSession();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
