namespace TicketManagement.Common.Entities
{
    public class EventFromJson
    {
        public string FullImagePath { get; set; }
        public Event Event { get; set; }
        public decimal Price { get; set; }
        public string EventLogoImage { get; set; }
    }
}
