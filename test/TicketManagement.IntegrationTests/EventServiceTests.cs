using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventServiceTests
    {
        private EventService _eventService;

        [SetUp]
        public void Setup()
        {
            _eventService = new EventService(new EventRepository());
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2023", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2023", "Stanger Things Serie")]
        public void Insert_WhenInsertEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _eventService.Insert(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2023", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2023", "Stanger Things Serie")]
        public void Update_WhenUpdateEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _eventService.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
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
        }

        [Test]
        public void GetAll_WhenHave3Entry_Should3Entry()
        {
            // arrange
            int expectedCount = 3;

            // act
            var actualCount = _eventService.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            int expectedId = 3;

            // act
            var actualId = _eventService.GetById(3);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }
    }
}
