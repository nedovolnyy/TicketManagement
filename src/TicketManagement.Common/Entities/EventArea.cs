﻿using System;

namespace TicketManagement.Common.Entities
{
    public sealed class EventArea : BaseEntity
    {
        public EventArea(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            Id = id;
            EventId = eventId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
            Price = price;
        }

        public int EventId { get; private set; }
        public string Description { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public decimal Price { get; private set; }
    }
}
