using System.Globalization;
using System.Security.Claims;
using EventManagementApiClientGenerated;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Serilog;
using TicketManagement.Common;
using TicketManagement.Common.Identity;
using TicketManagement.WebUI;
using TicketManagement.WebUI.Services;
using UserApiClientGenerated;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
if (!builder.Configuration.GetValue<bool>("UseReact"))
{
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

    services.AddLocalization(options => options.ResourcesPath = "Resources")
        .AddControllersWithViews()
        .AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(SharedResource));
        })
        .AddViewLocalization();

    services.AddAuthentication(options =>
    {
        options.DefaultScheme = Settings.Jwt.JwtOrCookieScheme;
        options.DefaultChallengeScheme = Settings.Jwt.JwtOrCookieScheme;
    })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.SlidingExpiration = true;
            options.LoginPath = "/Identity/Account/Login/";
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:5004";
                options.RequireHttpsMetadata = false;
                options.Audience = Settings.Jwt.JwtAudience;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[JwtBearerDefaults.AuthenticationScheme];
                        return Task.CompletedTask;
                    },
                };
            })
        .AddPolicyScheme(Settings.Jwt.JwtOrCookieScheme, Settings.Jwt.JwtOrCookieScheme, options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                string authorization = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                }

                return CookieAuthenticationDefaults.AuthenticationScheme;
            };
        });

    services.AddAuthorization(options =>
    {
        options.AddPolicy(nameof(Roles.Administrator), builder =>
        {
            builder.RequireClaim(ClaimTypes.Role, nameof(Roles.Administrator));
        });

        options.AddPolicy(nameof(Roles.EventManager), builder =>
        {
            builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, nameof(Roles.EventManager))
                                          || x.User.HasClaim(ClaimTypes.Role, nameof(Roles.Administrator)));
        });

        options.AddPolicy(nameof(Roles.User), builder =>
        {
            builder.RequireClaim(ClaimTypes.Role, nameof(Roles.User));
        });
    });

    services.AddControllersWithViews(options =>
    {
        options.Filters.Add<SerilogMvcLoggingAttribute>();
    });

    services.AddHttpClient("EventManagementApiClient")
        .ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(builder.Configuration["EventManagementApiAddress"]))
        .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new AreaManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new EventAreaManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new EventSeatManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new EventManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new LayoutManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new ThirdPartyEventApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new SeatManagementApiClient(baseUrl, httpClient);
    });
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("EventManagementApiClient");
        var baseUrl = builder.Configuration["EventManagementApiAddress"];
        return new VenueManagementApiClient(baseUrl, httpClient);
    });
    services.AddHttpClient("UsersAPIClient").ConfigureHttpClient((provider, c) => c.BaseAddress = new Uri(builder.Configuration["UserApiAddress"]))
        .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
    services.AddScoped(scope =>
    {
        var httpClient = scope.GetService<IHttpClientFactory>().CreateClient("UsersAPIClient");
        var baseUrl = builder.Configuration["UserApiAddress"];
        return new UsersManagementApiClient(baseUrl, httpClient);
    });
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddSingleton<ListThirdPartyEventsService>();
    services.AddTransient<BackendApiAuthenticationHttpClientHandler>();

    services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));

    services.AddMvc(options =>
    {
        options.Filters.Add(new RequireHttpsAttribute());
        options.Filters.Add<SerilogMvcLoggingAttribute>();
    });

    services.AddRazorPages();
    services.AddDatabaseDeveloperPageExceptionFilter();

    var app = builder.Build();

    app.UseRequestLocalization();

    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseStaticFiles();
    app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 5004));
    app.UseSession();
    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always,
    });
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.Use(async (context, next) =>
    {
        await next();
        var bearerAuth = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.StartsWith("Bearer ") ?? false;
        if (context.Response.StatusCode == 401
            && !context.User.Identity.IsAuthenticated
            && !bearerAuth)
        {
            await context.ChallengeAsync(JwtBearerDefaults.AuthenticationScheme);
        }
    });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
else
{
    services.AddSpaYarp();
    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseSpaYarp();
    app.MapFallbackToFile("index.html");

    app.Run();
}
