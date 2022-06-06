using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class AreaService : BaseService<Area>, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        public AreaService()
            : base(new AreaRepository())
        {
            _areaRepository = new AreaRepository();
        }

        internal AreaService(IAreaRepository areaRepository)
            : base(areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public override void Validate(Area entity)
        {
            var areaArray = _areaRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (var area in areaArray)
            {
                if (entity.Description == area.Description)
                {
                    throw new ValidationException("Area description should be unique for area!", "");
                }
            }
        }
    }
}
