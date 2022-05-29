using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class SeatService : BaseService<Seat>, IService<Seat>
    {
        private readonly SeatRepository _seatRepository;
        public SeatService()
            : base()
        {
            EntityRepository = new SeatRepository();
            _seatRepository = (SeatRepository)EntityRepository;
        }

        protected override BaseRepository<Seat> EntityRepository { get; }

        protected override void Validation(Seat entity)
        {
            IEnumerable<Seat> seatArray = _seatRepository.GetAllByAreaId(entity.AreaId);
            foreach (Seat seat in seatArray)
            {
                if ((entity.Row == seat.Row) && (entity.Number == seat.Number))
                {
                    throw new ValidationException("Row and number should be unique for area!", "");
                }
            }
        }
    }
}
