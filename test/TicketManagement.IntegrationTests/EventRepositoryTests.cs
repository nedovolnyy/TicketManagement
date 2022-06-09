using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventRepositoryTests
    {
        private readonly IEventRepository _evntRepository = new EventRepository(TestDatabaseFixture.DatabaseContext);

        [TestCase(1, "Stanger Things Serie", "09/19/2023", "Stanger Things Serie")]
        public void Insert_WhenInsertEvent_ShouldInt1(int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _evntRepository.Insert(new Event(0, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2023", "Kitchen Serie")]
        public void Update_WhenUpdateEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _evntRepository.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

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
                            () => _evntRepository.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _evntRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = _evntRepository.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = _evntRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(expectedCount, actualCount.Count);
        }
    }
}
