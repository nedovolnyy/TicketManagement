using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;

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

        public abstract Task<int> InsertAsync(T entity);

        public async Task UpdateAsync(T entity)
        {
            _databaseContext.Instance.Entry(entity).State = EntityState.Modified;
            await _databaseContext.Instance.SaveChangesAsync();
        }

        public abstract Task<int> DeleteAsync(int id);

        public abstract Task<T> GetByIdAsync(int id);

        public abstract IQueryable<T> GetAll();
    }
}
