using System.Text.Json;
using ThirdPartyEventEditor.Models;
using TicketManagement.Common.Entities;

namespace TicketManagement.MVC.Services
{
    public class ListThirdPartyEventsService
    {
        private readonly List<ThirdPartyEvent> _thirdPartyEvents = new List<ThirdPartyEvent>();

        public List<ThirdPartyEvent> Add(Event @event, decimal thirdPartyEventPrice)
        {
            _thirdPartyEvents.Remove(_thirdPartyEvents.Find(x => x.Name == @event.Name && x.EventTime == @event.EventTime));
            return _thirdPartyEvents;
        }

        public List<ThirdPartyEvent> Delete(string thirdPartyEventName, string thirdPartyEventDescription, DateTimeOffset thirdPartyEventTime)
        {
            _thirdPartyEvents.Remove(
                _thirdPartyEvents.Find(x => x.Name == thirdPartyEventName && x.EventTime == thirdPartyEventTime && x.Description == thirdPartyEventDescription));
            return _thirdPartyEvents;
        }

        public async Task<List<ThirdPartyEvent>> PreviewAsync(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var thirdPartyEvents = PrepareListOfThirdPartyEvents(await JsonSerializer.DeserializeAsync<List<ThirdPartyEvent>>(reader.BaseStream));
            return thirdPartyEvents;
        }

        private List<ThirdPartyEvent> PrepareListOfThirdPartyEvents(List<ThirdPartyEvent> thirdPartyEvents)
        {
            _thirdPartyEvents.Clear();
            foreach (var thirdPartyEvent in thirdPartyEvents)
            {
                _thirdPartyEvents.Add(thirdPartyEvent);
            }

            return _thirdPartyEvents;
        }
    }
}
