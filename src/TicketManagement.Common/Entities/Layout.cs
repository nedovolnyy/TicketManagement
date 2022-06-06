using System;

namespace TicketManagement.Common.Entities
{
    public sealed class Layout : BaseEntity
    {
        public Layout()
        {
        }

        public Layout(int id, string name, int venueId, string description)
        {
            Id = id;
            Name = name;
            VenueId = venueId;
            Description = description;
        }

        public string Name { get; private set; }
        public int VenueId { get; private set; }
        public string Description { get; private set; }
    }
}
