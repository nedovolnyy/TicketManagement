using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Common.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; protected set; }
    }
}
