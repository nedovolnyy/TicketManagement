using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;

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

        public async Task<int> Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.Instance.SaveChangesAsync();
            return 4;
        }

        public async Task<int> Update(T entity)
        {
            if (entity.Id is not 0)
            {
                var updateEntity = _dbSet.Find(entity.Id);
                if (updateEntity is not null)
                {
                    _dbSet.Update(updateEntity);
                    await _databaseContext.Instance.SaveChangesAsync();
                    return (int)EntityState.Modified;
                }
            }

            return default;
        }

        public async Task<int> Delete(int id)
        {
            var i = (int)_dbSet.Remove(await _dbSet.FindAsync(id)).State;
            await _databaseContext.Instance.SaveChangesAsync();
            return i;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
