using System.Collections.Generic;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.Services
{
    public class SeatService : IService<Seat>
    {
        private readonly SeatRepository _seatRepository;

        public SeatService()
        {
            _seatRepository = new SeatRepository();
        }

        public void Insert(Seat entity) =>
            _seatRepository.Insert(entity);

        public void Update(Seat entity) =>
            _seatRepository.Update(entity);

        public void Delete(int id) =>
            _seatRepository.Delete(id);
        public void Delete(Seat entity) =>
            _seatRepository.Delete(entity);

        public Seat GetById(int id) =>
            _seatRepository.GetById(id);

        public IEnumerable<Seat> GetAll() =>
            _seatRepository.GetAll();
    }
}
