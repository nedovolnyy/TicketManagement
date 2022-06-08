using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventAreaRepositoryTests
    {
        private readonly IEventAreaRepository _eventAreaRepository = new EventAreaRepository(TestDatabaseFixture.DatabaseContext);

        [TestCase(2, "Cinema Hall #1", 2, 1, 8.20)]
        public void Insert_WhenInsertEventArea_ShouldInt1(int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventAreaRepository.Insert(new EventArea(0, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1, 1, "Cinema Hall #2", 2, 1, 5.20)]
        public void Update_WhenUpdateEventArea_ShouldInt1(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _eventAreaRepository.Update(new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            // arrange
            var expectedException =
                "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_EventSeat\". " +
                "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.EventSeat\", column 'EventAreaId'.\r\n" +
                "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _eventAreaRepository.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventAreaRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _eventAreaRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
