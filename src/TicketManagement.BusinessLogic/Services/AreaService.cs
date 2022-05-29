using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class AreaService : IService<Area>
    {
        private readonly AreaRepository _areaRepository;

        public AreaService()
        {
            _areaRepository = new AreaRepository();
        }

        public void Insert(Area entity)
        {
            IEnumerable<Area> areaArray = _areaRepository.GetAllByLayoutId(entity.LayoutId);
            foreach (Area area in areaArray)
            {
                if (entity.Description == area.Description)
                {
                    throw new ValidationException("Area description should be unique for area!", "");
                }
            }

            _areaRepository.Insert(entity);
        }

        public void Update(Area entity) =>
            _areaRepository.Update(entity);
        public void Delete(int id) =>
            _areaRepository.Delete(id);
        public void Delete(Area entity) =>
            _areaRepository.Delete(entity);
        public Area GetById(int id) =>
            _areaRepository.GetById(id);
        public IEnumerable<Area> GetAll() =>
            _areaRepository.GetAll();
    }
}
