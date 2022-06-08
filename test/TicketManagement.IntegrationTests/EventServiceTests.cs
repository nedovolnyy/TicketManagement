using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventServiceTests
    {
        private readonly EventService _eventService = new EventService(new EventRepository(TestDatabaseFixture.DatabaseContext));

        [TestCase(2, "Kitchegerrthrgn Serie", "07/02/2023", "Kitchertrn Serie")]
        public void Insert_WhenInsertEvent_ShouldInt1(int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventService.Insert(new Event(0, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(2, 1, "StanegegerergThings Serie", "06/11/2023", "Stanerger Things Serie")]
        public void Update_WhenUpdateEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventService.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            // arrange
            var expectedException =
                "The DELETE statement conflicted with the REFERENCE constraint \"FK_Event_EventArea\". " +
                "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.EventArea\", column 'EventId'.\r\n" +
                "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _eventService.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = _eventService.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
