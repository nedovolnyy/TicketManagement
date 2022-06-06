using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class EventRepositoryTests
    {
        private IEventRepository _evntRepository;

        [SetUp]
        public void Setup()
        {
            _evntRepository = new EventRepository();
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2023", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2023", "Stanger Things Serie")]
        public void Insert_WhenInsertEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _evntRepository.Insert(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2023", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2023", "Stanger Things Serie")]
        public void Update_WhenUpdateEvent_ShouldInt1(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _evntRepository.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description));

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
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Event_EventArea\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'EventId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _evntRepository.Delete(id));

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
            var evnts = _evntRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(evnts.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // act
            int exc = 3;

            // actual
            var evnt = _evntRepository.GetById(3);

            // assert
            Assert.AreEqual(evnt.Id, exc);
        }

        [Test]
        public void GetAllByLayoutId_WhenHave2Entry_Should2Entry()
        {
            // act
            int exc = 2;

            // actual
            var evnts = _evntRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(evnts.Count, exc);
        }
    }
}
