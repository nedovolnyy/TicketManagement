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

        public void Insert(Layout entity) =>
            _layoutRepository.Insert(entity);

        public void Update(Layout entity) =>
            _layoutRepository.Update(entity);

        public void Delete(int id) =>
            _layoutRepository.Delete(id);
        public void Delete(Layout entity) =>
            _layoutRepository.Delete(entity);

        public Layout GetById(int id) =>
            _layoutRepository.GetById(id);

        public IEnumerable<Layout> GetAll() =>
            _layoutRepository.GetAll();
    }
}
