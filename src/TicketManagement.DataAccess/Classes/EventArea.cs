using System;
using System.Collections.Generic;
using System.Text;

namespace TicketManagement.BusinessLogic.Classes
{
    internal class EventArea
    {
        public int Id { get; }
        public int EventId { get; }
        public string Description { get; }
        public int CoordX { get; }
        public int CoordY { get; }
        public decimal Price { get; }
    }
}
