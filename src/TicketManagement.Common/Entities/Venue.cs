using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Venue")]
    public class Venue : BaseEntity
    {
        public Venue(int id, string name, string description, string address, string phone = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            Phone = phone;
        }

        [Required]
        [MaxLength(120)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; private set; }

        [MaxLength(30)]
        public string Phone { get; private set; }
    }
}
