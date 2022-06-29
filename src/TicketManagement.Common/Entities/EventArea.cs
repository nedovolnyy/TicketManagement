using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.Common.DI;

namespace TicketManagement.Common.Entities
{
    [Table("EventArea")]
    public class EventArea : BaseEntity, IEventArea
    {
        public EventArea(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            Id = id;
            EventId = eventId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
            Price = price;
        }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int CoordX { get; set; }

        [Required]
        public int CoordY { get; set; }

        [Required]
        [Column("Price", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
