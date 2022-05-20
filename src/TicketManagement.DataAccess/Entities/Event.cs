using System;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Entities
{
    public class Event : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LayoutId { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
