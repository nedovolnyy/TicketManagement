using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity, IAggregateRoot
    {
        int Insert(T entity, string insertSql, SqlTransaction sqlTransaction);
        int Update(T entity, string updateSql, SqlTransaction sqlTransaction);
        int Delete(int id, string deleteSql, SqlTransaction sqlTransaction);
        T GetById(int id, string getByIdSql);
        IEnumerable<T> GetAll(string getAllSql);
    }
}
