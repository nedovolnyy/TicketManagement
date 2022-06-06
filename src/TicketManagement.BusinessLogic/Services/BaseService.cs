using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal abstract class BaseService<T> : IService<T>
        where T : BaseEntity
    {
        private readonly IRepository<T> _entityRepository;
        protected BaseService()
        {
        }

        protected BaseService(IRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Insert(T entity)
        {
            Validate(entity);
            _entityRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            Validate(entity);
            _entityRepository.Update(entity);
        }

        public void Delete(int id) =>
            _entityRepository.Delete(id);
        public T GetById(int id) =>
            _entityRepository.GetById(id);
        public IEnumerable<T> GetAll() =>
            _entityRepository.GetAll();

        public abstract void Validate(T entity);
    }
}
