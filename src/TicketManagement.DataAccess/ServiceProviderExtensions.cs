using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Identity;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        ////private IConfiguration Configuration { get; set; } = null!;
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IDatabaseContext, DatabaseContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>(
                        options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                            options.Password.RequireUppercase = false;
                            options.Password.RequiredLength = 4;
                            options.SignIn.RequireConfirmedAccount = false;
                            ////options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                            ////options.Lockout.MaxFailedAccessAttempts = 5;
                            ////options.Lockout.AllowedForNewUsers = true;
                        })
                    .AddRoles<Role>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<DatabaseContext>();

            services.AddTransient<IAreaRepository, AreaRepository>(provider => new AreaRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<IEventAreaRepository, EventAreaRepository>(provider => new EventAreaRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<IEventSeatRepository, EventSeatRepository>(provider => new EventSeatRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<IEventRepository, EventRepository>(provider => new EventRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<ILayoutRepository, LayoutRepository>(provider => new LayoutRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<ISeatRepository, SeatRepository>(provider => new SeatRepository(provider.GetRequiredService<IDatabaseContext>()));

            services.AddTransient<IVenueRepository, VenueRepository>(provider => new VenueRepository(provider.GetRequiredService<IDatabaseContext>()));
        }
    }
}