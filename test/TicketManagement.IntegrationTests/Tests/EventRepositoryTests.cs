using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class EventRepositoryTests
    {
        private EventRepository _evntRepository;

        [SetUp]
        public void Setup()
        {
            _evntRepository = new EventRepository();
        }

        [Test]
        public void Event_GetAll()
        {
            // act
            int exc = 3;

            // actual
            var evnts = _evntRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(evnts.Count, exc);
        }

        [Test]
        public void Event_GetById()
        {
            // act
            int exc = 3;

            // actual
            var evnt = _evntRepository.GetById(3);

            // assert
            Assert.AreEqual(evnt.Id, exc);
        }

        [Test]
        public void Event_GetAllByLayoutId()
        {
            // act
            int exc = 2;

            // actual
            var evnts = _evntRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(evnts.Count, exc);
        }
    }
}
