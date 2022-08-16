using System.ComponentModel.DataAnnotations;
using TicketManagement.WebUI.Helpers;

namespace TicketManagement.WebUI.Models
{
    public class EventModel
    {
        public EventModel()
        {
        }

        public EventModel(DateTime eventTime,
                          DateTime eventEndTime,
                          string name,
                          string description,
                          string eventLogoImage,
                          decimal price,
                          List<string> layoutsId)
        {
            Name = name;
            EventTime = eventTime;
            Description = description;
            LayoutsId = layoutsId;
            EventEndTime = eventEndTime;
            EventLogoImage = eventLogoImage;
            Price = price;
        }

        [Required]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Time")]
        public DateTime EventTime { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Layout")]
        public List<string> LayoutsId { get; set; }

        [Required]
        [Display(Name = "Event End Time")]
        [IgnoreValidation]
        public DateTime EventEndTime { get; set; }

        [Required]
        [Display(Name = "EventLogoImage")]
        public string EventLogoImage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
