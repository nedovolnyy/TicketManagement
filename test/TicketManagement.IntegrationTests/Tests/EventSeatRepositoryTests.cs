using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class EventSeatRepositoryTests
    {
        private EventSeatRepository _eventSeatRepository;

        [SetUp]
        public void Setup()
        {
            _eventSeatRepository = new EventSeatRepository();
        }

        [Test]
        public void EventSeat_GetAll()
        {
            // act
            int exc = 9;

            // actual
            var eventSeats = _eventSeatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(eventSeats.Count, exc);
        }

        [Test]
        public void EventSeat_GetById()
        {
            // act
            int exc = 3;

            // actual
            var eventSeat = _eventSeatRepository.GetById(3);

            // assert
            Assert.AreEqual(eventSeat.Id, exc);
        }
    }
}
