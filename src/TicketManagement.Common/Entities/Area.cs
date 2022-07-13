using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Area")]
    public class Area : BaseEntity
    {
        public Area()
        {
        }

        public Area(int layoutId, string description, int coordX, int coordY)
            : this(default, layoutId, description, coordX, coordY)
        {
        }

        public Area(int id, int layoutId, string description, int coordX, int coordY)
        {
            Id = id;
            LayoutId = layoutId;
            Description = description;
            CoordX = coordX;
            CoordY = coordY;
        }

        [Required]
        [ForeignKey("Layout")]
        public int LayoutId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public int CoordX { get; set; }
        [Required]
        public int CoordY { get; set; }
    }
}
