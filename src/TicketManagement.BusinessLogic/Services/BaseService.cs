using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    public abstract class BaseService<T> : IService<T>
        where T : BaseEntity, new()
    {
        protected BaseService()
        {
        }

        protected abstract IRepository<T> EntityRepository { get; }

        public void Insert(T entity)
        {
            Validation(entity);
            EntityRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            Validation(entity);
            EntityRepository.Update(entity);
        }

        public void Delete(int id) =>
            EntityRepository.Delete(id);
        public void Delete(T entity) =>
            EntityRepository.Delete(entity);
        public T GetById(int id) =>
            EntityRepository.GetById(id);
        public IEnumerable<T> GetAll() =>
            EntityRepository.GetAll();

        protected abstract void Validation(T entity);
    }
}
