using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class AreaService : BaseService<Area>, IService<Area>
    {
        private readonly AreaRepository _areaRepository;
        public AreaService()
            : base()
        {
            EntityRepository = new AreaRepository();
            _areaRepository = (AreaRepository)EntityRepository;
        }

        protected override BaseRepository<Area> EntityRepository { get; }

        protected override void Validation(Area entity)
        {
            IEnumerable<Area> areaArray = _areaRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (Area area in areaArray)
            {
                if (entity.Description == area.Description)
                {
                    throw new ValidationException("Area description should be unique for area!", "");
                }
            }
        }
    }
}
