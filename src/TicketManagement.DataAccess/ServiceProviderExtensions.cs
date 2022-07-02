using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.DataAccess.EF;
using TicketManagement.DataAccess.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        ////private IConfiguration Configuration { get; set; } = null!;
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddIdentity<IdentityUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<DatabaseContext>();

            services.AddTransient<IAreaRepository>(provider =>
            {
                return new AreaRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<IEventAreaRepository>(provider =>
            {
                return new EventAreaRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<IEventSeatRepository>(provider =>
            {
                return new EventSeatRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<EventRepository>(provider =>
            {
                return new EventRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<ILayoutRepository>(provider =>
            {
                return new LayoutRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<ISeatRepository>(provider =>
            {
                return new SeatRepository(provider.GetRequiredService<DatabaseContext>());
            });

            services.AddTransient<IVenueRepository>(provider =>
            {
                return new VenueRepository(provider.GetRequiredService<DatabaseContext>());
            });
        }
    }
}