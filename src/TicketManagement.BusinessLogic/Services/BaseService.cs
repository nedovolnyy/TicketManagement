using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.DI;

namespace TicketManagement.BusinessLogic.Services
{
    internal abstract class BaseService<T> : IService<T>
        where T : class, IBaseEntity
    {
        private readonly IRepository<T> _entityRepository;

        protected BaseService(IRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<int> InsertAsync(T entity)
        {
            await ValidateAsync(entity);
            return await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await ValidateAsync(entity);
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(int id) =>
            await _entityRepository.DeleteAsync(id);
        public virtual async Task<T> GetByIdAsync(int id) =>
            await _entityRepository.GetByIdAsync(id);
        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _entityRepository.GetAll().ToListAsyncSafe();

        public abstract Task ValidateAsync(T entity);
    }
}
