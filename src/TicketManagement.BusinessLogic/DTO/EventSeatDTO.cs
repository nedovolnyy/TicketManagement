using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class EventSeatDto : BaseDto
    {
        public Guid EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
