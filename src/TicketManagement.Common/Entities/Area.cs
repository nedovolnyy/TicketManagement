using System.Collections.Generic;

namespace TicketManagement.Common.Entities
{
    public class Area : BaseEntity
    {
        public Area(int id, int layoutId, string description, int coordX, int coordY)
        {
            Id = id;
            LayoutId = layoutId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
            Seats = new HashSet<Seat>();
        }

        public int LayoutId { get; private set; }
        public string Description { get; private set; }
        public int CoordX { get; private set; }
        public int CoordY { get; private set; }
        public Layout Layout { get; private set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
