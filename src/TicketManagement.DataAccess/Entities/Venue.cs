using System;
using System.Collections.Generic;
using System.Text;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class Venue : BaseEntity, IAggregateRoot
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
