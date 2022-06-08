using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class SeatService : BaseService<Seat>, ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        public SeatService(ISeatRepository seatRepository)
            : base(seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public override void Validate(Seat entity)
        {
            if ((entity.AreaId == 0) | (entity.Row == 0) | (entity.Number == 0))
            {
                throw new ValidationException("The field of Seat is not allowed to be null!");
            }

            var seatArray = _seatRepository.GetAllByAreaId(entity.AreaId);
            foreach (var seat in seatArray)
            {
                if ((entity.Row == seat.Row) && (entity.Number == seat.Number))
                {
                    throw new ValidationException("Row and number should be unique for area!");
                }
            }
        }
    }
}
