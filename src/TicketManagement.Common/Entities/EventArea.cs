using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public class EventArea : BaseEntity
    {
        public EventArea(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            Id = id;
            EventId = eventId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
            Price = price;
            EventSeats = new HashSet<EventSeat>();
        }

        public int EventId { get; private set; }
        public string Description { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public decimal Price { get; private set; }
        public Event Event { get; private set; }
        public virtual ICollection<EventSeat> EventSeats { get; set; }
    }
}
