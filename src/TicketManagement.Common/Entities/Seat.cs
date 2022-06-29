using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketManagement.Common.DI;

namespace TicketManagement.Common.Entities
{
    [Table("Seat")]
    public class Seat : BaseEntity, ISeat
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
        public int AreaId { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
