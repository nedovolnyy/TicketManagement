using System;

namespace TicketManagement.Common.Entities
{
    public class Event : BaseEntity
    {
        public Event(int id, string name, DateTimeOffset eventTime, string description, int layoutId)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
            Description = description;
            LayoutId = layoutId;
        }

        public string Name { get; private set; }
        public DateTimeOffset EventTime { get; private set; }
        public string Description { get; private set; }
        public int LayoutId { get; private set; }
    }
}
