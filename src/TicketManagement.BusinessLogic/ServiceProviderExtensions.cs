using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaService, AreaService>();

            services.AddTransient<IEventAreaService, EventAreaService>();

            services.AddTransient<IEventSeatService, EventSeatService>();

            services.AddTransient<IEventService, EventService>();

            services.AddTransient<ILayoutService, LayoutService>();

            services.AddTransient<ISeatService, SeatService>();

            services.AddTransient<IThirdPartyEventService, ThirdPartyEventService>();

            services.AddTransient<IVenueService, VenueService>();
        }
    }
}