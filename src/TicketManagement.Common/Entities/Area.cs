using System;

namespace TicketManagement.Common.Entities
{
    public class Area : BaseEntity
    {
        public Area()
        {
        }

        public Area(int? id, int? layoutId, string description, int? coordX, int? coordY)
        {
            Id = id;
            LayoutId = layoutId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
        }

        public int? LayoutId { get; private set; }
        public string Description { get; private set; }
        public int? CoordX { get; private set; }
        public int? CoordY { get; private set; }
        public virtual Layout Layout { get; private set; }
        protected override string ForEquals(BaseEntity entity) =>
            LayoutId + Description + CoordX + CoordY;
        protected override bool IsNull(BaseEntity entity) =>
                   LayoutId == null
                 & Description == null
                 & CoordX == null
                 & CoordY == null;

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
