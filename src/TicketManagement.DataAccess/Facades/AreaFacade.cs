using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.DataAccess.Facades
{
    public class AreaFacade
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AreaFacade(IAreaRepository repository, IUnitOfWork uow)
        {
            _areaRepository = repository;
            _unitOfWork = uow;
        }

        public int Save(Area area)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Insert into Area (LayoutId, Description, CoordX, CoordY) VALUES (@LayoutId, @Description, @CoordX, @CoordY)";
                i = _areaRepository.Insert(area, strSql, sqlTransaction);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public int Update(Area area)
        {
            int i = 0;
            try
            {
                SqlTransaction sqlTransaction = _unitOfWork.BeginTransaction();
                string strSql = "Update Area set LayoutId = @LayoutId, Description = @Description, CoordX = @CoordX, CoordY = @CoordY Where Id = @Id";
                i = _areaRepository.Update(area, strSql, sqlTransaction);
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
                string strSql = "Delete from Area Where Id = @Id";
                i = _areaRepository.Delete(id, strSql, sqlTransaction);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }

        public Area GetById(int id)
        {
            string strSql = "select * from dbo.Area where id = @id";
            return _areaRepository.GetById(id, strSql);
        }

        public IEnumerable<Area> GetAll()
        {
            return _areaRepository.GetAll("SELECT * FROM Area");
        }
    }
}
