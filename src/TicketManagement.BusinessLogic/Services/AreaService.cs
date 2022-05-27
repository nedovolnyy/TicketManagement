using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
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

        public void Insert(Area dto) =>
            _areaRepository.Insert(dto);

        public void Update(Area dto) =>
            _areaRepository.Update(dto);

        public void Delete(int id) =>
            _areaRepository.Delete(id);

        public Area GetById(int id) =>
            _areaRepository.GetById(id);

        public IEnumerable<Area> GetAll() =>
            _areaRepository.GetAll();
    }
}
