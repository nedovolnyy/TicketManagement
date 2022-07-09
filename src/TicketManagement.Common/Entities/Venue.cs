using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Venue")]
    public class Venue : BaseEntity
    {
        public Venue()
        {
        }

        public Venue(string name, string description, string address, string phone = null)
            : this(default, name, description, address, phone)
        {
        }

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
        public string Name { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }
    }
}
