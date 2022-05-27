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

        public void Insert(Seat dto) =>
            _seatRepository.Insert(dto);

        public void Update(Seat dto) =>
            _seatRepository.Update(dto);

        public void Delete(int id) =>
            _seatRepository.Delete(id);

        public Seat GetById(int id) =>
            _seatRepository.GetById(id);

        public IEnumerable<Seat> GetAll() =>
            _seatRepository.GetAll();
    }
}
