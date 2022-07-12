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
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IDatabaseContext, DatabaseContext>(
                options => options.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                    .AddIdentity<User, Role>(
                        options =>
                        {
                            options.Password.RequireDigit = true;
                            options.Password.RequireLowercase = true;
                            options.Password.RequireNonAlphanumeric = true;
                            options.Password.RequireUppercase = false;
                            options.Password.RequiredLength = 6;
                            options.SignIn.RequireConfirmedAccount = false;
                        })
                    .AddRoles<Role>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<DatabaseContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

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