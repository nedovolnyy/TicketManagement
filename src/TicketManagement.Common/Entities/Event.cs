using System;
using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public class Event : BaseEntity
    {
        public Event(int id, string name, DateTimeOffset eventTime, string description, int layoutId, DateTime eventEndTime)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
            Description = description;
            LayoutId = layoutId;
            EventEndTime = eventEndTime;
            EventAreas = new HashSet<EventArea>();
        }

        public string Name { get; private set; }
        public DateTimeOffset EventTime { get; private set; }
        public string Description { get; private set; }
        public int LayoutId { get; private set; }
        public DateTime EventEndTime { get; private set; }
        public Layout Layout { get; private set; }
        public virtual ICollection<EventArea> EventAreas { get; set; }
    }
}
