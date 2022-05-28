using System;
using System.Collections;

namespace TicketManagement.Common.Entities
{
    public class EventArea : BaseEntity
    {
        public EventArea()
        {
        }

        public EventArea(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            Id = id;
            EventId = eventId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
            Price = price;
        }

        public int EventId { get; private set; }
        public string Description { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public decimal Price { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                EventId+Description+CoordX+CoordY+Price;
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
