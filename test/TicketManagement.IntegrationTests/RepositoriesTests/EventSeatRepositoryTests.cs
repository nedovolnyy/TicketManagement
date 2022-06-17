using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventSeatRepositoryTests
    {
        private readonly IEventSeatRepository _eventSeatRepository = new EventSeatRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public void Insert_WhenInsertEventSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatRepository.Insert(new EventSeat(0, 1, 9, 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateEventSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatRepository.Update(new EventSeat(3, 1, 3, 3, 2));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatRepository.Delete(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventSeatRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = _eventSeatRepository.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
