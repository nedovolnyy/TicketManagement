using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Event")]
    public class Event : BaseEntity
    {
        public Event(int id, string name, DateTimeOffset eventTime, string description, int layoutId, DateTime eventEndTime)
        {
            Id = id;
            Name = name;
            EventTime = eventTime;
            Description = description;
            LayoutId = layoutId;
            EventEndTime = eventEndTime;
        }

        [Required]
        [MaxLength(120)]
        public string Name { get; private set; }

        [Required]
        public DateTimeOffset EventTime { get; private set; }

        [Required]
        public string Description { get; private set; }

        [Required]
        [ForeignKey("Layout")]
        public int LayoutId { get; private set; }

        [Required]
        public DateTime EventEndTime { get; private set; }
    }
}
