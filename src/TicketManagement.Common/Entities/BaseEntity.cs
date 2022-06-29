using System.ComponentModel.DataAnnotations;
using TicketManagement.Common.DI;

namespace TicketManagement.Common.Entities
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
