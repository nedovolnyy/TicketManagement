using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
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

        [TestCase(1, 1, "Cinema Hall #2", 2, 1, 5.20)]
        [TestCase(1, 2, "Cinema Hall #1", 2, 1, 8.20)]
        public void Insert_WhenInsertEventArea_ShouldInt1(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _eventAreaRepository.Insert(new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1, 1, "Cinema Hall #2", 2, 1, 5.20)]
        [TestCase(1, 2, "Cinema Hall #1", 2, 1, 8.20)]
        public void Update_WhenUpdateEventArea_ShouldInt1(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _eventAreaRepository.Update(new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_EventSeat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventSeat\", column 'EventAreaId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventAreaRepository.Delete(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void GetAll_WhenHave3Entry_Should3Entry()
        {
            // act
            int exc = 3;

            // actual
            var eventAreas = _eventAreaRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(eventAreas.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
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
