using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Common.Entities
{
    public class EventWithPrice
    {
        public EventWithPrice()
        {
        }

        public EventWithPrice(Event @event)
        {
            @Event = @event;
            Price = decimal.Zero;
        }

        public EventWithPrice(Event @event,
                          decimal price)
        {
            @Event = @event;
            Price = price;
        }

        [Required]
        [Display(Name = "Event")]
        public Event Event { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
