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

            services.AddTransient<IEventService, EventService>(provider => new EventService(provider.GetRequiredService<IEventRepository>()));

            services.AddTransient<ILayoutService, LayoutService>(provider => new LayoutService(provider.GetRequiredService<ILayoutRepository>()));

            services.AddTransient<ISeatService, SeatService>(provider => new SeatService(provider.GetRequiredService<ISeatRepository>()));

            services.AddTransient<IThirdPartyEventService, ThirdPartyEventService>(provider => new ThirdPartyEventService(provider.GetRequiredService<IEventRepository>()));

            services.AddTransient<IVenueService, VenueService>(provider => new VenueService(provider.GetRequiredService<IVenueRepository>()));
        }
    }
}