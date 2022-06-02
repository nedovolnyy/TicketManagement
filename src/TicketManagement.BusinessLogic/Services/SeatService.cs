using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class SeatService : BaseService<Seat>
    {
        private readonly ISeatRepository _seatRepository;
        internal SeatService()
        {
            EntityRepository = new SeatRepository();
            _seatRepository = (ISeatRepository)EntityRepository;
        }

        protected override IRepository<Seat> EntityRepository { get; set; }

        protected override void Validate(Seat entity)
        {
            var seatArray = _seatRepository.GetAllByAreaId(entity.AreaId);
            foreach (var seat in seatArray)
            {
                if ((entity.Row == seat.Row) && (entity.Number == seat.Number))
                {
                    throw new ValidationException("Row and number should be unique for area!", "");
                }
            }
        }
    }
}
