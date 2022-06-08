using System;

namespace TicketManagement.Common.Entities
{
    public sealed class EventSeat : BaseEntity
    {
        public EventSeat(int id, int eventAreaId, int row, int number, int state)
        {
            Id = id;
            EventAreaId = eventAreaId;
            Row = row;
            Number = number;
            State = state;
        }

        public int EventAreaId { get; private set; }
        public int Row { get; private set; }
        public int Number { get; private set; }
        public int State { get; private set; }
    }
}
