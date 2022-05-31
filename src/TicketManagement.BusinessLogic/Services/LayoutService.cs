using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class LayoutService : BaseService<Layout>, IService<Layout>
    {
        private readonly ILayoutRepository _layoutRepository;
        public LayoutService()
            : base()
        {
            EntityRepository = new LayoutRepository();
            _layoutRepository = (ILayoutRepository)EntityRepository;
        }

        protected override IRepository<Layout> EntityRepository { get; }

        protected override void Validation(Layout entity)
        {
            IEnumerable<Layout> layoutArray = _layoutRepository.GetAllByVenueId(entity.VenueId);
            foreach (Layout layout in layoutArray)
            {
                if (entity.Description == layout.Description)
                {
                    throw new ValidationException("Layout name should be unique in venue!", "");
                }
            }
        }
    }
}
