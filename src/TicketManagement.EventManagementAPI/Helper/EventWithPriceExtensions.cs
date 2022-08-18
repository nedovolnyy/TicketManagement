using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.EventManagementAPI.Helper
{
    public static class EventRepositoryResolver
    {
        private static IServiceProvider _serviceProvider;
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T Resolve<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public static bool TryResolve<T>(out T service)
        {
            try
            {
                service = _serviceProvider.GetService<T>();
                return true;
            }
            catch (Exception)
            {
                service = default;
                return false;
            }
        }
    }

    /// <summary>
    /// Helper methods for collection of the event object.
    /// </summary>
    public static class EventWithPriceExtensions
    {
        private static readonly IEventRepository _eventRepository = EventRepositoryResolver.Resolve<IEventRepository>();

        public static async Task InsertAsync(this IEnumerable<EventWithPrice> eventWithPrices, EventWithPrice eventWithPrice)
            => await _eventRepository.InsertAsync(eventWithPrice.Event, eventWithPrice.Price);

        public static async Task UpdateAsync(this IEnumerable<EventWithPrice> eventWithPrices, EventWithPrice eventWithPrice)
            => await _eventRepository.UpdateAsync(eventWithPrice.Event, eventWithPrice.Price);

        public static async Task DeleteAsync(this IEnumerable<EventWithPrice> eventWithPrices, int eventId)
            => await _eventRepository.DeleteAsync(eventId);

        public static async Task<EventWithPrice> GetByIdAsync(this IEnumerable<EventWithPrice> eventWithPrices, int eventId)
            => new EventWithPrice(await _eventRepository.GetByIdAsync(eventId));

        public static async Task<IEnumerable<EventWithPrice>> GetAllAsync(this IEnumerable<EventWithPrice> eventWithPrices)
        {
            var tempEventWithPrices = new List<EventWithPrice>();
            var events = await _eventRepository.GetAll().ToListAsyncSafe();
            events.ForEach(@event => tempEventWithPrices.Add(new EventWithPrice(@event)));

            return tempEventWithPrices;
        }

        public static async Task<IEnumerable<EventWithPrice>> GetAllByLayoutIdAsync(this IEnumerable<EventWithPrice> eventWithPrices, int layoutId)
            => (IEnumerable<EventWithPrice>)await _eventRepository.GetAllByLayoutId(layoutId).ToListAsyncSafe();

        public static async Task<bool> IsAllAvailableSeatsAsync(this IEnumerable<EventWithPrice> eventWithPrices, int id)
            => await _eventRepository.IsAllAvailableSeatsAsync(id);

        public static async Task<decimal> GetPriceByEventIdAsync(this IEnumerable<EventWithPrice> eventWithPrices, int id)
            => await _eventRepository.GetPriceByEventIdAsync(id);

        public static async Task<int> GetSeatsAvailableCountAsync(this IEnumerable<EventWithPrice> eventWithPrices, int id)
            => await _eventRepository.GetSeatsAvailableCountAsync(id);
        public static async Task<int> GetSeatsCountAsync(this IEnumerable<EventWithPrice> eventWithPrices, int layoutId)
            => await _eventRepository.GetSeatsCountAsync(layoutId);
    }
}
