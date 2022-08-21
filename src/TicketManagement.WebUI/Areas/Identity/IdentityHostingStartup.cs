using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Identity;
using TicketManagement.UserAPI.DataAccess;

[assembly: HostingStartup(typeof(TicketManagement.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace TicketManagement.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UserApiDbContext>(
                options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>(
                        options =>
                        {
                            options.SignIn.RequireConfirmedAccount = false;
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

                ////var tokenSettings = context.Configuration.GetSection(nameof(JwtTokenSettings));
                ////services.AddAuthentication(options =>
                ////{
                ////    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                ////    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                ////    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                ////})
                ////    .AddJwtBearer(options =>
                ////    {
                ////        options.RequireHttpsMetadata = false;
                ////        options.TokenValidationParameters = new TokenValidationParameters
                ////        {
                ////            ValidateIssuer = true,
                ////            ValidIssuer = tokenSettings[nameof(JwtTokenSettings.JwtIssuer)],
                ////            ValidateAudience = true,
                ////            ValidAudience = tokenSettings[nameof(JwtTokenSettings.JwtAudience)],
                ////            ValidateIssuerSigningKey = true,
                ////            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings[nameof(JwtTokenSettings.JwtSecretKey)])),
                ////            ValidateLifetime = false,
                ////        };
                ////        options.SaveToken = true;
                ////    });

                ////services.Configure<JwtTokenSettings>(tokenSettings);
                ////services.AddScoped<JwtTokenService>();
            });
        }
    }
}