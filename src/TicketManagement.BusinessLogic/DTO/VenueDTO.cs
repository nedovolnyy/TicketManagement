using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class VenueDto : BaseDto
    {
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
