using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class EventSeat : BaseEntity, IAggregateRoot
    {
        public Guid EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
