using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class LayoutService : BaseService<Layout>
    {
        private readonly ILayoutRepository _layoutRepository;
        internal LayoutService()
        {
            EntityRepository = new LayoutRepository();
            _layoutRepository = (ILayoutRepository)EntityRepository;
        }

        protected override IRepository<Layout> EntityRepository { get; set; }

        protected override void Validate(Layout entity)
        {
            var layoutArray = _layoutRepository.GetAllByVenueId(entity.VenueId);
            foreach (var layout in layoutArray)
            {
                if (entity.Description == layout.Description)
                {
                    throw new ValidationException("Layout name should be unique in venue!", "");
                }
            }
        }
    }
}
