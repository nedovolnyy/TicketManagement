using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.Services
{
    internal abstract class BaseService<T> : IService<T>
        where T : BaseEntity
    {
        protected abstract IRepository<T> EntityRepository { get; set; }

        public void Insert(T entity)
        {
            Validate(entity);
            EntityRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            Validate(entity);
            EntityRepository.Update(entity);
        }

        public void Delete(int id) =>
            EntityRepository.Delete(id);
        public T GetById(int id) =>
            EntityRepository.GetById(id);
        public IEnumerable<T> GetAll() =>
            EntityRepository.GetAll();

        protected abstract void Validate(T entity);
    }
}
