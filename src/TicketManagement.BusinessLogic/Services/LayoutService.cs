using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class LayoutService : IService<Layout>
    {
        private readonly LayoutRepository _layoutRepository;

        public LayoutService()
        {
            _layoutRepository = new LayoutRepository();
        }

        public void Insert(Layout dto) =>
            _layoutRepository.Insert(dto);

        public void Update(Layout dto) =>
            _layoutRepository.Update(dto);

        public void Delete(int id) =>
            _layoutRepository.Delete(id);

        public Layout GetById(int id) =>
            _layoutRepository.GetById(id);

        public IEnumerable<Layout> GetAll() =>
            _layoutRepository.GetAll();
    }
}
