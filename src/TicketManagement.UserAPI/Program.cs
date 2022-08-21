using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TicketManagement.Common.DI;
using TicketManagement.Common.Identity;
using TicketManagement.UserAPI.DataAccess;
using TicketManagement.UserAPI.Services;
using TicketManagement.UserAPI.Settings;

var roles = new string[] { "Administrator", "EventManager" };

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.File(builder.Configuration["Logging:FilePath"], rollingInterval: RollingInterval.Day)
                        .WriteTo.Console()
                        .CreateLogger();
builder.Host.UseSerilog(logger);

////builder.Services.Configure<HostOptions>(hostOptions =>
////        {
////            hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
////        });

builder.WebHost.UseUrls("https://*:5000").ConfigureKestrel(options =>
{
    options.ListenAnyIP(5004, configure => configure.UseHttps());
});

builder.Services.AddDbContext<UserApiDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>()
                    .AddRoles<Role>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<UserApiDbContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

var tokenSettings = builder.Configuration.GetSection(nameof(JwtTokenSettings));
builder.Services.AddAuthentication(options =>
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
builder.Services.AddAuthorization(options =>
{
    foreach (var prop in roles)
    {
        options.AddPolicy(prop, policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, roles));
    }
});

builder.Services.Configure<JwtTokenSettings>(tokenSettings);
builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddControllers();

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
////    {
////        { jwtSecurityScheme, Array.Empty<string>() },
////    });
////});
builder.Services.AddOpenApiDocument();

var app = builder.Build();

JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

app.UseSerilogRequestLogging();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
////app.UseSwagger();
////app.UseSwaggerUI(options =>
////{
////    options.SwaggerEndpoint("/swagger/v1/swagger.json", "User API v1");
////});

app.UseOpenApi();
app.UseSwaggerUi3();
////app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    ////endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    ////{
    ////    Predicate = check => check.Tags.Contains("live"),
    ////}).WithMetadata(new AllowAnonymousAttribute());
});

app.Run();