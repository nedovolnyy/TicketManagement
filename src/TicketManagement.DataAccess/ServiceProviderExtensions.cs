using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
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
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<IAreaRepository, AreaRepository>();

            services.AddTransient<IEventAreaRepository, EventAreaRepository>();

            services.AddTransient<IEventSeatRepository, EventSeatRepository>();

            services.AddTransient<IEventRepository, EventRepository>();

            services.AddTransient<ILayoutRepository, LayoutRepository>();

            services.AddTransient<ISeatRepository, SeatRepository>();

            services.AddTransient<IVenueRepository, VenueRepository>();
        }
    }
}