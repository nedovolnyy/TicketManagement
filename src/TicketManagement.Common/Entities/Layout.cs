using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public class Layout : BaseEntity
    {
        public Layout(int id, string name, int venueId, string description)
        {
            Id = id;
            Name = name;
            VenueId = venueId;
            Description = description;
            Areas = new HashSet<Area>();
            Events = new HashSet<Event>();
        }

        public string Name { get; private set; }
        public int VenueId { get; private set; }
        public string Description { get; private set; }
        public Venue Venue { get; private set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
