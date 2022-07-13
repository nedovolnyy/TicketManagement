using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Common.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
