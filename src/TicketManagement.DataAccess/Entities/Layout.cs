using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class Layout : BaseEntity, IAggregateRoot
    {
        public int VenueId { get; set; }
        public string Description { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
