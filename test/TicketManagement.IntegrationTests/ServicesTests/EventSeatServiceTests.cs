using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventSeatServiceTests
    {
        private readonly EventSeatService _eventSeatService = new EventSeatService(new EventSeatRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public void Insert_WhenInsertEventSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatService.Insert(new EventSeat(0, 1, 9, 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateEventSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatService.Update(new EventSeat(2, 1, 3, 3, 2));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventSeatService.Delete(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventSeatService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 4;

            // act
            var actualId = _eventSeatService.GetById(4);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
