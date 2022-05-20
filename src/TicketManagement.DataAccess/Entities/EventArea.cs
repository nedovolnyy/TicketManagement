using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class EventArea : BaseEntity, IAggregateRoot
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public decimal Price { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
