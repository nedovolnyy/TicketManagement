using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

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
            var seatArray = _seatRepository.GetAllByAreaId(entity.AreaId);
            foreach (var seat in seatArray)
            {
                if (entity.Row == seat.Row && entity.Number == seat.Number)
                {
                    throw new ValidationException("Row and number should be unique for area!");
                }
            }

            if (entity.AreaId == default)
            {
                throw new ValidationException("The field 'AreaId' of Seat is not allowed to be null!");
            }
            else if (entity.Row == default)
            {
                throw new ValidationException("The field 'Row' of Seat is not allowed to be null!");
            }
            else
            {
                throw new ValidationException("The field 'Number' of Seat is not allowed to be null!");
            }
        }
    }
}
