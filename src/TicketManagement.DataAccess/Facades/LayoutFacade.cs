using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class LayoutFacade
    {
        private readonly IRepository<Layout> _layoutRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LayoutFacade(IRepository<Layout> repository, IUnitOfWork uow)
        {
            _layoutRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(Layout layout)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into Layout (VenueId, Description) VALUES (@VenueId, @Description)";
                i = _layoutRepository.Insert(layout, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(Layout layout)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update Layout set VenueId = @VenueId, Description = @Description Where Id = @Id";
                i = _layoutRepository.Update(layout, strSql, sqlTransaction);
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
                string strSql = "Delete from Layout Where Id = @Id";
                i = _layoutRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public Layout GetById(int id)
        {
            string strSql = "select * from dbo.Layout where id = @id";
            return _layoutRepository.GetById(id, strSql);
        }

        public IEnumerable<Layout> GetAll()
        {
            return _layoutRepository.GetAll("SELECT * FROM Layout");
        }
    }
}
