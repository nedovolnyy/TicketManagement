using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class EventAreaDto : BaseDto
    {
        public Guid EventId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public decimal Price { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
