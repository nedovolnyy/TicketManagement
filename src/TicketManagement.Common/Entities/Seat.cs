using System;

namespace TicketManagement.Common.Entities
{
    public sealed class Seat : BaseEntity
    {
        public Seat(int id, int areaId, int row, int number)
        {
            Id = id;
            AreaId = areaId;
            Row = row;
            Number = number;
        }

        public int AreaId { get; private set; }
        public int Row { get; private set; }
        public int Number { get; private set; }
    }
}
