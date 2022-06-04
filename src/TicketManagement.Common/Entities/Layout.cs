﻿using System;

namespace TicketManagement.Common.Entities
{
    public sealed class Layout : BaseEntity
    {
        public Layout()
        {
        }

        public Layout(int? id, int? venueId, string description)
        {
            Id = id;
            VenueId = venueId;
            Description = description;
        }

        public int? VenueId { get; private set; }
        public string Description { get; private set; }
    }
}
