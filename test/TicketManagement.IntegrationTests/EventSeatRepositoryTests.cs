using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventSeatRepositoryTests
    {
        private IEventSeatRepository _eventSeatRepository;

        [SetUp]
        public void Setup()
        {
            _eventSeatRepository = new EventSeatRepository();
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Insert_WhenFKConstraint_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string expectedException =
                    "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                    "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                    "The statement has been terminated.";

                // act
                var actualException = Assert.Throws<SqlException>(
                                () => _eventSeatRepository.Insert(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(actualException.Message, Is.EqualTo(expectedException));
            }
        }

        [TestCase(2, 6, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Update_WhenFKConstraint_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string expectedException =
                    "The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                    "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                    "The statement has been terminated.";

                // act
                var actualException = Assert.Throws<SqlException>(
                                () => _eventSeatRepository.Update(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(actualException.Message, Is.EqualTo(expectedException));
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _eventSeatRepository.Delete(id);

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [Test]
        public void GetAll_WhenHave9Entry_Should9Entry()
        {
            // arrange
            int expectedCount = 9;

            // act
            var actualCount = _eventSeatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            int expectedId = 3;

            // act
            var actualId = _eventSeatRepository.GetById(3);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }
    }
}
