using TicketManagement.BusinessLogic.Assembler;
using TicketManagement.BusinessLogic.DTO;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.DataAccess.Facades;

namespace TicketManagement.BusinessLogic.Services
{
    public class AreaService : IService<AreaDto>
    {
        private readonly AreaFacade _areaFacade;
        private readonly AreaAssembler _areaAssembler;

        public AreaService(AreaFacade areaFacade, AreaAssembler areaAssembler)
        {
            _areaFacade = areaFacade;
            _areaAssembler = areaAssembler;
        }

        public AreaDto Get(int id)
        {
            var areaDto = _areaFacade.GetById(id);
            return _areaAssembler.WriteDto(areaDto);
        }
    }
}
