using System;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private EventService _evntService;

        [SetUp]
        public void Setup()
        {
            _evntService = new EventService();
        }

        [TestCase(2, 1, "Stanger Things Serie", "2022-06-08 00:00:00.0000000 +00:00", "Stanger Things Serie")]
        public void EventService_Insert_WhenEventInSameTimeLayout_ShouldThrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Do not create event for the same layout in the same time!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Insert(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(2, 1, "Stanger Things Serie", "2022-06-08 00:00:00.0000000 +00:00", "Stanger Things Serie")]
        public void EventService_Update_WhenEventInSameTimeLayout_ShouldThrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Do not create event for the same layout in the same time!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2021", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie")]
        public void EventService_Insert_WhenEventInPast_ShouldThrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Event can't be created in the past!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Insert(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2021", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie")]
        public void EventService_Update_WhenEventInPast_ShouldThrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Event can't be created in the past!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Update(new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventService_GetById_WhenNonExistentId_ShouldThrow(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have events to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }
    }
}
