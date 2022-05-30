using System;

namespace TicketManagement.Common.Entities
{
    public class EventSeat : BaseEntity
    {
        public EventSeat()
        {
        }

        public EventSeat(int? id, int? eventAreaId, int? row, int? number, int? state)
        {
            Id = id;
            EventAreaId = eventAreaId;
            Row = row;
            Number = number;
            State = state;
        }

        public int? EventAreaId { get; private set; }
        public int? Row { get; private set; }
        public int? Number { get; private set; }
        public int? State { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                EventAreaId.ToString() + Row + Number + State;
        protected override bool IsNull(BaseEntity entity) =>
                   EventAreaId == null
                 & Row == null
                 & Number == null
                 & State == null;
    }
}
