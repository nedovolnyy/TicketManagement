﻿using System;
using System.Collections;

namespace TicketManagement.Common.Entities
{
    public class Layout : BaseEntity
    {
        public Layout()
        {
        }

        public Layout(int id, int venueId, string description)
        {
            Id = id;
            VenueId = venueId;
            Description = description;
        }

        public int VenueId { get; private set; }
        public string Description { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                VenueId+Description;

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
