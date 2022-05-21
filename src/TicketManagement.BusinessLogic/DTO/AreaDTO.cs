using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class AreaDto : BaseDto
    {
        public Guid LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
