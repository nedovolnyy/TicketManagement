using System;

namespace TicketManagement.Common.Entities
{
    public class Seat : BaseEntity
    {
        public Seat()
        {
        }

        public Seat(int? id, int? areaId, int? row, int? number)
        {
            Id = id;
            AreaId = areaId;
            Row = row;
            Number = number;
        }

        public int? AreaId { get; private set; }
        public int? Row { get; private set; }
        public int? Number { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
                AreaId.ToString()+Row+Number;
        protected override bool IsNull(BaseEntity entity) =>
                   AreaId == null
                 & Row == null
                 & Number == null;
    }
}
