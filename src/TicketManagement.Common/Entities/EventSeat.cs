using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("EventSeat")]
    public class EventSeat : BaseEntity
    {
        public EventSeat(int id, int eventAreaId, int row, int number, int state)
        {
            Id = id;
            EventAreaId = eventAreaId;
            Row = row;
            Number = number;
            State = state;
        }

        [Required]
        [ForeignKey("EventArea")]
        public int EventAreaId { get; private set; }

        [Required]
        public int Row { get; private set; }

        [Required]
        public int Number { get; private set; }

        [Required]
        public int State { get; private set; }
    }
}
