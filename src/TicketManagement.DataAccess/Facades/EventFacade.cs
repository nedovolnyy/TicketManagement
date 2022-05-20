using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class EventFacade
    {
        private readonly IEventRepository _evntRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventFacade(IEventRepository repository, IUnitOfWork uow)
        {
            _evntRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(Event evnt)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into Event (Name, Description, LayoutId) VALUES (@Name, @Description, @LayoutId)";
                i = _evntRepository.Insert(evnt, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(Event evnt)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update Event set Name = @Name, Description = @Description, LayoutId = @LayoutId Where Id = @Id";
                i = _evntRepository.Update(evnt, strSql, sqlTransaction);
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
                string strSql = "Delete from Event Where Id = @Id";
                i = _evntRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public Event GetById(int id)
        {
            string strSql = "select * from dbo.Event where id = @id";
            return _evntRepository.GetById(id, strSql);
        }

        public IEnumerable<Event> GetAll()
        {
            return _evntRepository.GetAll("SELECT * FROM Event");
        }
    }
}
