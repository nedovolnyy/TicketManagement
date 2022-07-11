using System.ComponentModel.DataAnnotations;
using TicketManagement.MVC.Helpers;

namespace TicketManagement.MVC.Models
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
                          string price,
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
        [Display(Name = "Description for Area")]
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
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public string Price { get; set; }
    }
}
