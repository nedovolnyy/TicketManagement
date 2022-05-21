using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class SeatFacade
    {
        private readonly IRepository<Seat> _seatRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeatFacade(IRepository<Seat> repository, IUnitOfWork uow)
        {
            _seatRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(Seat seat)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into Seat (AreaId, Row, Number) VALUES (@AreaId, @Row, @Number)";
                i = _seatRepository.Insert(seat, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(Seat seat)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update Seat set AreaId = @AreaId, Row = @Row, Number = @Number Where Id = @Id";
                i = _seatRepository.Update(seat, strSql, sqlTransaction);
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
                string strSql = "Delete from Seat Where Id = @Id";
                i = _seatRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public Seat GetById(int id)
        {
            string strSql = "select * from dbo.Seat where id = @id";
            return _seatRepository.GetById(id, strSql);
        }

        public IEnumerable<Seat> GetAll()
        {
            return _seatRepository.GetAll("SELECT * FROM Seat");
        }
    }
}
