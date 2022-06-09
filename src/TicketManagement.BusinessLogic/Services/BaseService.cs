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

        protected BaseService(IRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual int Insert(T entity)
        {
            Validate(entity);
            return _entityRepository.Insert(entity);
        }

        public virtual int Update(T entity)
        {
            Validate(entity);
            return _entityRepository.Update(entity);
        }

        public virtual int Delete(int id) =>
            _entityRepository.Delete(id);
        public virtual T GetById(int id) =>
            _entityRepository.GetById(id);
        public virtual IEnumerable<T> GetAll() =>
            _entityRepository.GetAll();

        public abstract void Validate(T entity);
    }
}
