using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class SeatService : BaseService<ISeat>, ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        public SeatService(ISeatRepository seatRepository)
            : base(seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public override async Task Validate(ISeat entity)
        {
            if (entity.AreaId == default)
            {
                throw new ValidationException("The field 'AreaId' of Seat is not allowed to be null!");
            }
            else if (entity.Row == default)
            {
                throw new ValidationException("The field 'Row' of Seat is not allowed to be null!");
            }
            else if (entity.Number == default)
            {
                throw new ValidationException("The field 'Number' of Seat is not allowed to be null!");
            }
            else
            {
                var seatArray = await _seatRepository.GetAllByAreaId(entity.AreaId);
                foreach (var seat in seatArray)
                {
                    if (entity.Row == seat.Row && entity.Number == seat.Number)
                    {
                        throw new ValidationException("Row and number should be unique for area!");
                    }
                }
            }
        }
    }
}
