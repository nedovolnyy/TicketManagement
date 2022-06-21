using Microsoft.Extensions.DependencyInjection;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess
{
    public static class ServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
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
