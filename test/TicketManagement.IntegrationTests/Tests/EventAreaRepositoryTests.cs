using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class EventAreaRepositoryTests
    {
        private IEventAreaRepository _eventAreaRepository;

        [SetUp]
        public void Setup()
        {
            _eventAreaRepository = new EventAreaRepository();
        }

        [Test]
        public void EventArea_GetAll()
        {
            // act
            int exc = 3;

            // actual
            var eventAreas = _eventAreaRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(eventAreas.Count, exc);
        }

        [Test]
        public void EventArea_GetById()
        {
            // act
            int exc = 1;

            // actual
            var eventArea = _eventAreaRepository.GetById(1);

            // assert
            Assert.AreEqual(eventArea.Id, exc);
        }
    }
}
