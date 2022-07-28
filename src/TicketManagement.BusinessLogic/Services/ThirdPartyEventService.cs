using System;
using System.IO;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Services
{
    public class ThirdPartyEventService : IThirdPartyEventService
    {
        private readonly IEventRepository _eventRepository;
        public ThirdPartyEventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task InsertAsync(string fullImagePath, Event @event, decimal price, string evntLogoImage)
        {
            var imgBytes = Convert.FromBase64String(evntLogoImage[(evntLogoImage.LastIndexOf(',') + 1)..]);

            using var imageFile = new FileStream(fullImagePath, FileMode.Create);
            imageFile.Write(imgBytes, 0, imgBytes.Length);
            imageFile.Flush();

            await _eventRepository.InsertAsync(@event, price);
        }
    }
}
