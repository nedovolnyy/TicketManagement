using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class EventAreaFacade
    {
        private readonly IRepository<EventArea> _eventEventAreaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventAreaFacade(IRepository<EventArea> repository, IUnitOfWork uow)
        {
            _eventEventAreaRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(EventArea eventEventArea)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into EventArea (EventId, Description, CoordX, CoordY, Price) VALUES (@EventId, @Description, @CoordX, @CoordY, @Price)";
                i = _eventEventAreaRepository.Insert(eventEventArea, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(EventArea eventEventArea)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update EventArea set EventId = @EventId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY, Price = @Price Where Id = @Id";
                i = _eventEventAreaRepository.Update(eventEventArea, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Delete(int id)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Delete from EventArea Where Id = @Id";
                i = _eventEventAreaRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public EventArea GetById(int id)
        {
            string strSql = "select * from dbo.EventArea where id = @id";
            return _eventEventAreaRepository.GetById(id, strSql);
        }

        public IEnumerable<EventArea> GetAll()
        {
            return _eventEventAreaRepository.GetAll("SELECT * FROM EventArea");
        }
    }
}
