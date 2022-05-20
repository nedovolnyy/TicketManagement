using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class EventSeatFacade
    {
        private readonly IEventSeatRepository _eventSeatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventSeatFacade(IEventSeatRepository repository, IUnitOfWork uow)
        {
            _eventSeatRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(EventSeat eventSeat)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into EventSeat (EventAreaId, Row, Number, State) VALUES (@EventAreaId, @Row, @Number, @State)";
                i = _eventSeatRepository.Insert(eventSeat, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(EventSeat eventSeat)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update EventSeat set EventAreaId = @EventAreaId, Row = @Row, Number = @Number, State = @State Where Id = @Id";
                i = _eventSeatRepository.Update(eventSeat, strSql, sqlTransaction);
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
                string strSql = "Delete from EventSeat Where Id = @Id";
                i = _eventSeatRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public EventSeat GetById(int id)
        {
            string strSql = "select * from dbo.EventSeat where id = @id";
            return _eventSeatRepository.GetById(id, strSql);
        }

        public IEnumerable<EventSeat> GetAll()
        {
            return _eventSeatRepository.GetAll("SELECT * FROM EventSeat");
        }
    }
}
