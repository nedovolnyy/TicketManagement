using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class Seat : BaseEntity, IAggregateRoot
    {
        public int AreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
