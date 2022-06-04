using System;

namespace TicketManagement.Common.Entities
{
    public sealed class Area : BaseEntity
    {
        public Area()
        {
        }

        public Area(int id, int layoutId, string description, int coordX, int coordY)
        {
            Id = id;
            LayoutId = layoutId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
        }

        public int LayoutId { get; private set; }
        public string Description { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
    }
}
