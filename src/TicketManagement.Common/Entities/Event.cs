using System;

namespace TicketManagement.Common.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
        }

        public Event(int id, string name, string description, int layoutId)
        {
            Id = id;
            Name = name;
            Description = description;
            LayoutId = layoutId;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int LayoutId { get; private set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
