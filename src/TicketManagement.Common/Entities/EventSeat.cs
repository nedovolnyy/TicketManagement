using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("EventSeat")]
    public class EventSeat : BaseEntity
    {
        public EventSeat()
        {
        }

        public EventSeat(int eventAreaId, int row, int number, State state)
            : this(default, eventAreaId, row, number, state)
        {
        }

        public EventSeat(int id, int eventAreaId, int row, int number, State state)
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
        public State State { get; set; }
    }
}
