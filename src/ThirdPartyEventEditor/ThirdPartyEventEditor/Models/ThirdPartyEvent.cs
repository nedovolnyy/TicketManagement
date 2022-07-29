namespace ThirdPartyEventEditor.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ThirdPartyEvent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(DateTimeOffset), "1/1/2022", "1/1/2040")]
        public DateTimeOffset EventTime { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2022", "1/1/2040")]
        public DateTime EventEndTime { get; set; }

        [Required]
        [StringLength(9166, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public int LayoutId { get; set; }

        [Required]
        public string EventLogoImage { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}