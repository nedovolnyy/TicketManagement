using System;
using System.Collections.Generic;
using System.Text;

namespace TicketManagement.BusinessLogic.Classes
{
    internal class EventSeat
    {
        public int Id { get; }
        public int EventAreaId { get; }
        public int Row { get; }
        public int Number { get; }
        public int State { get; }
    }
}
