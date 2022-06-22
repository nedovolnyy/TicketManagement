using Microsoft.Extensions.DependencyInjection;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;

namespace TicketManagement.BusinessLogic
{
    public static class ServiceProviderExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IEventAreaService, EventAreaService>();
            services.AddTransient<IEventSeatService, EventSeatService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ILayoutService, LayoutService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IVenueService, VenueService>();
        }
    }
}
