using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaService, AreaService>(provider => new AreaService(provider.GetRequiredService<IAreaRepository>()));

            services.AddTransient<IEventAreaService, EventAreaService>(provider => new EventAreaService(provider.GetRequiredService<IEventAreaRepository>()));

            services.AddTransient<IEventSeatService, EventSeatService>(provider => new EventSeatService(provider.GetRequiredService<IEventSeatRepository>()));

            services.AddTransient<IEventService, EventService>(provider => new EventService(provider.GetRequiredService<IEventRepository>()));

            services.AddTransient<ILayoutService, LayoutService>(provider => new LayoutService(provider.GetRequiredService<ILayoutRepository>()));

            services.AddTransient<ISeatService, SeatService>(provider => new SeatService(provider.GetRequiredService<ISeatRepository>()));

            services.AddTransient<IVenueService, VenueService>(provider => new VenueService(provider.GetRequiredService<IVenueRepository>()));
        }
    }
}