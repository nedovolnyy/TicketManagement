using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicketManagement.Common.Identity;
using TicketManagement.UserAPI.DataAccess;
using TicketManagement.UserAPI.Services;
using TicketManagement.UserAPI.Settings;

namespace TicketManagement.UserAPI
{
    public static class ClaimPermission
    {
        public static readonly string[] Roles = { "Administrator", "EventManager" };
    }

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ////services.AddHealthChecks().AddCheck(
            ////    "current_api_check",
            ////    () => HealthCheckResult.Healthy("User API is alive"),
            ////    new[] { "live" });

            services.AddDbContext<UserApiDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>(
                        options =>
                        {
                            ////options.Password.RequireDigit = true;
                            ////options.Password.RequireLowercase = true;
                            ////options.Password.RequireNonAlphanumeric = true;
                            ////options.Password.RequireUppercase = false;
                            ////options.Password.RequiredLength = 6;
                            ////options.SignIn.RequireConfirmedAccount = false;
                        })
                    .AddRoles<Role>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<UserApiDbContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

            var tokenSettings = _configuration.GetSection(nameof(JwtTokenSettings));
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
                    foreach (var prop in typeof(ClaimPermission).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                    {
                        options.AddPolicy(prop.GetValue(null).ToString(), policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, ClaimPermission.Roles));
                    }
                });

            services.Configure<JwtTokenSettings>(tokenSettings);
            services.AddScoped<JwtTokenService>();

            services.AddControllers();

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
            services.AddOpenApiDocument();
        }

        public void Configure(IApplicationBuilder app)
        {
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

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
        }
    }
}
