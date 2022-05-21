using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class SeatDto : BaseDto
    {
        public Guid AreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
