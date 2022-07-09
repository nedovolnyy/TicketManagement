using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Event")]
    public class Event : BaseEntity
    {
        public Event()
        {
        }

        public Event(
            string name,
            DateTimeOffset eventTime,
            string description,
            int layoutId,
            DateTime eventEndTime,
            string eventLogoImage)
            : this(default, name, eventTime, description, layoutId, eventEndTime, eventLogoImage)
        {
        }

        public Event(
            int id,
            string name,
            DateTimeOffset eventTime,
            string description,
            int layoutId,
            DateTime eventEndTime,
            string eventLogoImage)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
            Description = description;
            LayoutId = layoutId;
            EventEndTime = eventEndTime;
            EventLogoImage = eventLogoImage;
        }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset EventTime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Layout")]
        public int LayoutId { get; set; }

        [Required]
        public DateTime EventEndTime { get; set; }

        [Required]
        public string EventLogoImage { get; set; }
    }
}
