using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class Area : BaseEntity, IAggregateRoot
    {
        public Guid LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
