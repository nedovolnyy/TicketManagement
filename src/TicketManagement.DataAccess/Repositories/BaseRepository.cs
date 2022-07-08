using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.DataAccess.Repositories
{
    internal class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;
        protected BaseRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Instance.Set<T>();
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.Instance.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _databaseContext.Instance.Entry(entity).State = EntityState.Modified;
            await _databaseContext.Instance.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            _dbSet.Remove(await _dbSet.FindAsync(id));
            await _databaseContext.Instance.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
