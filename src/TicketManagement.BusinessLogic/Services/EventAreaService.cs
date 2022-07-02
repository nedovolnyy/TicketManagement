using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class EventAreaService : BaseService<IEventArea>, IEventAreaService
    {
        public EventAreaService(IEventAreaRepository eventAreaRepository)
            : base(eventAreaRepository)
        {
        }

        public override async Task ValidateAsync(IEventArea entity)
        {
            if (entity.EventId == default)
            {
                throw new ValidationException("The field 'LayoutId' of EventArea is not allowed to be null!");
            }

            if (entity.CoordX == default)
            {
                throw new ValidationException("The field 'CoordX' of EventArea is not allowed to be null!");
            }

            if (entity.CoordY == default)
            {
                throw new ValidationException("The field 'CoordY' of EventArea is not allowed to be null!");
            }

            if (entity.Price == default)
            {
                throw new ValidationException("The field 'Price' of EventArea is not allowed to be null!");
            }

            if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of EventArea is not allowed to be empty!");
            }

            await Task.Delay(100);
        }
    }
}
