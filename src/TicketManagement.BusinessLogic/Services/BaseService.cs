using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.Services
{
    internal abstract class BaseService<T> : IService<T>
        where T : BaseEntity
    {
        private readonly IRepository<T> _entityRepository;

        protected BaseService(IRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task InsertAsync(T entity)
        {
            await ValidateAsync(entity);
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await ValidateAsync(entity);
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(int id) =>
            await _entityRepository.DeleteAsync(id);
        public virtual async Task<T> GetByIdAsync(int id) =>
            await _entityRepository.GetByIdAsync(id);
        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _entityRepository.GetAll().ToListAsyncSafe();

        public abstract Task ValidateAsync(T entity);
    }
}
