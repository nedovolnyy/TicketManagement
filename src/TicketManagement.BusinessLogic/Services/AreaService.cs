using System;
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
            if ((entity.LayoutId == 0) | (entity.CoordX == 0) | (entity.CoordY == 0) | (entity.Description == ""))
            {
                throw new ValidationException("The field of Area is not allowed to be null!", "");
            }

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
