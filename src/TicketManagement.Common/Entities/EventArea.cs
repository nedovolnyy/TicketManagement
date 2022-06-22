using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("EventArea")]
    public class EventArea : BaseEntity
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
        public int EventId { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; private set; }

        [Required]
        public int CoordX { get; private set; }

        [Required]
        public int CoordY { get; private set; }

        [Required]
        [Column("Price", TypeName = "decimal(18, 2)")]
        public decimal Price { get; private set; }
    }
}
