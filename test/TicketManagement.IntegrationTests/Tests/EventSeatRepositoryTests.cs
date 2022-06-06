using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
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
                string strException =
                    "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventSeatRepository.Insert(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(2, 6, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Update_WhenFKConstraint_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventSeatRepository.Update(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _eventSeatRepository.Delete(id);

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GetAll_WhenHave9Entry_Should9Entry()
        {
            // act
            int exc = 9;

            // actual
            var eventSeats = _eventSeatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(eventSeats.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
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
