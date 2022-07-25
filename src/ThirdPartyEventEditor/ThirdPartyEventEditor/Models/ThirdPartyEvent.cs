using System;
using System.ComponentModel.DataAnnotations;

namespace ThirdPartyEventEditor.Models
{
    public class ThirdPartyEvent
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset EventTime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int LayoutId { get; set; }

        [Required]
        public DateTime EventEndTime { get; set; }

        [Required]
        public string EventLogoImage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}