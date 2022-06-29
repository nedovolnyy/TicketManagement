using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
        where T : class, IBaseEntity
    {
        private readonly IDatabaseContext _databaseContext;
        protected BaseRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public virtual async Task<int> Insert(T entity)
        {
            await _databaseContext.Instance.SaveChangesAsync();
            return (int)EntityState.Added;
        }

        public async Task<int> Update(T entity)
        {
            _databaseContext.Instance.Entry(entity).State = EntityState.Modified;
            await _databaseContext.Instance.SaveChangesAsync();
            return (int)EntityState.Modified;
        }

        public virtual async Task<int> Delete(int id)
        {
            await _databaseContext.Instance.SaveChangesAsync();
            return default;
        }

        public abstract Task<T> GetById(int id);

        public abstract Task<IEnumerable<T>> GetAll();
    }
}
