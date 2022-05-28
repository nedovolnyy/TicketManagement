using TicketManagement.BusinessLogic.DTO;

namespace TicketManagement.BusinessLogic.Interfaces
{
    public interface IAreaService
    {
        Country Create(Country country);
        Country CreateWithUsers(Country country, IList<User> users);
        void Delete(Country country);
        void Delete(int countryId);
        Country Edit(Country country);
        Country FindByName(string name);
        IEnumerable<Country> List();
        AreaDto GetArea(int id);
    }
}
