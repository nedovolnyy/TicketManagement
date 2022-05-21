using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class LayoutDto : BaseDto
    {
        public Guid VenueId { get; set; }
        public string Description { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
