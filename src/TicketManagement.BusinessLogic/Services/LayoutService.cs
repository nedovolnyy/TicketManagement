using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    internal class LayoutService : BaseService<Layout>, ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;

        internal LayoutService()
            : base(new LayoutRepository())
        {
            _layoutRepository = new LayoutRepository();
        }

        public LayoutService(ILayoutRepository layoutRepository)
            : base(layoutRepository)
        {
            _layoutRepository = layoutRepository;
        }

        public override void Validate(Layout entity)
        {
            var layoutArray = _layoutRepository.GetAllByVenueId(entity.VenueId);
            foreach (var layout in layoutArray)
            {
                if (entity.Name == layout.Name)
                {
                    throw new ValidationException("Layout name should be unique in venue!", "");
                }
            }
        }
    }
}
