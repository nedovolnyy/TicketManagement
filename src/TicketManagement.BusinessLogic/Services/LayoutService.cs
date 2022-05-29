using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Validation;
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

        public void Insert(Layout entity)
        {
            IEnumerable<Layout> layoutArray = _layoutRepository.GetAllByVenueId(entity.VenueId);
            foreach (Layout layout in layoutArray)
            {
                if (entity.Description == layout.Description)
                {
                        throw new ValidationException("Layout name should be unique in venue!", "");
                }
            }

            _layoutRepository.Insert(entity);
        }

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
