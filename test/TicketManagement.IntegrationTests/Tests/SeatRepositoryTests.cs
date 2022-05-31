using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class SeatRepositoryTests
    {
        private SeatRepository _seatRepository;

        [SetUp]
        public void Setup()
        {
            _seatRepository = new SeatRepository();
        }

        [Test]
        public void Seat_GetAll()
        {
            // act
            int exc = 6;

            // actual
            var seats = _seatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(seats.Count, exc);
        }

        [Test]
        public void Seat_GetById()
        {
            // act
            int exc = 1;

            // actual
            var seat = _seatRepository.GetById(1);

            // assert
            Assert.AreEqual(seat.Id, exc);
        }

        [Test]
        public void Seat_GetAllByLayoutId()
        {
            // act
            int exc = 5;

            // actual
            var seats = _seatRepository.GetAllByAreaId(1).ToList();

            // assert
            Assert.AreEqual(seats.Count, exc);
        }
    }
}
