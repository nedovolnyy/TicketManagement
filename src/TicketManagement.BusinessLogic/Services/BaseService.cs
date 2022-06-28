using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Common.Entities;
using TicketManagement.DI;

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

        public virtual async Task<int> Insert(T entity)
        {
            await Validate(entity);
            return await _entityRepository.Insert(entity);
        }

        public virtual async Task<int> Update(T entity)
        {
            await Validate(entity);
            return await _entityRepository.Update(entity);
        }

        public virtual async Task<int> Delete(int id) =>
            await _entityRepository.Delete(id);
        public virtual async Task<T> GetById(int id) =>
            await _entityRepository.GetById(id);
        public virtual async Task<IEnumerable<T>> GetAll() =>
            await _entityRepository.GetAll();

        public abstract Task Validate(T entity);
    }
}
