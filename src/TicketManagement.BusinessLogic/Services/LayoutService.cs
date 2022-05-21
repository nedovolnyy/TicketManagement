using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class LayoutService : IService<LayoutDto>
    {
        private readonly LayoutFacade _layoutFacade;
        private readonly LayoutAssembler _layoutAssembler;

        public LayoutService(LayoutFacade layoutFacade, LayoutAssembler layoutAssembler)
        {
            _layoutFacade = layoutFacade;
            _layoutAssembler = layoutAssembler;
        }

        public LayoutDto Get(int id)
        {
            var layoutDto = _layoutFacade.GetById(id);
            return _layoutAssembler.WriteDto(layoutDto);
        }
    }
}
