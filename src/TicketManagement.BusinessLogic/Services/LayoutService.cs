using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal class LayoutService : BaseService<Layout>, ILayoutService
    {
        private readonly ILayoutRepository _layoutRepository;
        public LayoutService(ILayoutRepository layoutRepository)
            : base(layoutRepository)
        {
            _layoutRepository = layoutRepository;
        }

        public override void Validate(Layout entity)
        {
            if (entity.VenueId == default)
            {
                throw new ValidationException("The field 'VenueId' of Layout is not allowed to be null!");
            }
            else if (string.IsNullOrEmpty(entity.Name))
            {
                throw new ValidationException("The field 'Name' of Layout is not allowed to be empty!");
            }
            else if (string.IsNullOrEmpty(entity.Description))
            {
                throw new ValidationException("The field 'Description' of Layout is not allowed to be empty!");
            }
            else
            {
                var layoutArray = _layoutRepository.GetAllByVenueId(entity.VenueId);
                foreach (var layout in layoutArray)
                {
                    if (entity.Name == layout.Name)
                    {
                        throw new ValidationException("Layout name should be unique in venue!");
                    }
                }
            }
        }
    }
}
