using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal class AreaService : BaseService<Area>, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        internal AreaService(IAreaRepository areaRepository)
            : base(areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public override void Validate(Area entity)
        {
            if (entity.LayoutId == default)
            {
                throw new ValidationException("The field 'LayoutId' of Area is not allowed to be null!");
            }
            else if (entity.CoordX == default)
            {
                throw new ValidationException("The field 'CoordX' of Area is not allowed to be null!");
            }
            else if (entity.CoordY == default)
            {
                throw new ValidationException("The field 'CoordY' of Area is not allowed to be null!");
            }
            else if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Area is not allowed to be empty!");
            }
            else
            {
                var areaArray = _areaRepository.GetAllByLayoutId(entity.LayoutId);
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
}
