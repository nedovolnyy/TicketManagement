using System;

namespace TicketManagement.BusinessLogic.DTO
{
    public class EventDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LayoutId { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
