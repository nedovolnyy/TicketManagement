using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.DataAccess.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        ////private IConfiguration Configuration { get; set; } = null!;
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IAreaService>(provider => new AreaService(provider.GetRequiredService<IAreaRepository>()));

            services.AddTransient<IEventAreaService>(provider => new EventAreaService(provider.GetRequiredService<IEventAreaRepository>()));

            services.AddTransient<IEventSeatService>(provider => new EventSeatService(provider.GetRequiredService<IEventSeatRepository>()));

            services.AddTransient<EventService>(provider => new EventService(provider.GetRequiredService<EventRepository>()));

            services.AddTransient<ILayoutService>(provider =>
            {
                return new LayoutService(provider.GetRequiredService<ILayoutRepository>());
            });

            services.AddTransient<ISeatService>(provider =>
            {
                return new SeatService(provider.GetRequiredService<ISeatRepository>());
            });

            services.AddTransient<IVenueService>(provider =>
            {
                return new VenueService(provider.GetRequiredService<IVenueRepository>());
            });
        }
    }
}