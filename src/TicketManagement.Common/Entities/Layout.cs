using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Layout")]
    public class Layout : BaseEntity
    {
        public Layout(int id, string name, int venueId, string description)
        {
            Id = id;
            Name = name;
            VenueId = venueId;
            Description = description;
        }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Area")]
        public int VenueId { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
    }
}
