using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.Services
{
    internal class AreaService : BaseService<Area>, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        public AreaService(IAreaRepository areaRepository)
            : base(areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public override async Task ValidateAsync(Area entity)
        {
            if (entity.LayoutId == default)
            {
                throw new ValidationException("The field 'LayoutId' of Area is not allowed to be null!");
            }

            if (entity.CoordX == default)
            {
                throw new ValidationException("The field 'CoordX' of Area is not allowed to be null!");
            }

            if (entity.CoordY == default)
            {
                throw new ValidationException("The field 'CoordY' of Area is not allowed to be null!");
            }

            if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Area is not allowed to be empty!");
            }

            var areaArray = await _areaRepository.GetAllByLayoutId(entity.LayoutId).ToListAsyncSafe();
            foreach (var area in areaArray)
            {
                if (entity.Description == area.Description)
                {
                    throw new ValidationException("Area description should be unique for area!");
                }
            }
        }
    }
}
