using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagement.Common.Entities
{
    [Table("Seat")]
    public class Seat : BaseEntity
    {
        public Seat(int id, int areaId, int row, int number)
        {
            Id = id;
            AreaId = areaId;
            Row = row;
            Number = number;
        }

        [Required]
        [ForeignKey("Area")]
        public int AreaId { get; private set; }

        [Required]
        public int Row { get; private set; }

        [Required]
        public int Number { get; private set; }
    }
}
