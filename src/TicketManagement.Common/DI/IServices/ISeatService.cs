using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface ISeatService : IService<ISeat>
    {
        Task ValidateAsync(ISeat entity);
    }
}
