using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.Common.DI;

namespace TicketManagement.Common.Entities
{
    [Table("EventSeat")]
    public class EventSeat : BaseEntity, IEventSeat
    {
        public EventSeat(int id, int eventAreaId, int row, int number, bool state)
        {
            Id = id;
            EventAreaId = eventAreaId;
            Row = row;
            Number = number;
            State = state;
        }

        [Required]
        [ForeignKey("EventArea")]
        public int EventAreaId { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool State { get; set; }
    }
}
