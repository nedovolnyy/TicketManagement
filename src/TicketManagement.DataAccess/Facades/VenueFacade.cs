using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class VenueFacade
    {
        private readonly IVenueRepository _venueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VenueFacade(IVenueRepository repository, IUnitOfWork uow)
        {
            _venueRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(Venue venue)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into Venue (Description, Address, Phone) VALUES (@Description, @Address, @Phone)";
                i = _venueRepository.Insert(venue, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(Venue venue)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update Venue set Description = @Description, Address = @Address, Phone = @Phone Where Id = @Id";
                i = _venueRepository.Update(venue, strSql, sqlTransaction);
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
                string strSql = "Delete from Venue Where Id = @Id";
                i = _venueRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public Venue GetById(int id)
        {
            string strSql = "select * from dbo.Venue where id = @id";
            return _venueRepository.GetById(id, strSql);
        }

        public IEnumerable<Venue> GetAll()
        {
            return _venueRepository.GetAll("SELECT * FROM Venue");
        }
    }
}
