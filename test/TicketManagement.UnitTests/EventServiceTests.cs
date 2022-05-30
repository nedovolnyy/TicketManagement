using System;
using System.Data.SqlClient;
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

        [TestCase(1, 2, "First evnt of second layout", "jjl")]
        [TestCase(2, 1, "First evnt of first layout", "fhkh")]
        [TestCase(3, 2, "First evnt of second layout", "gjlgj")]
        public void EventService_Insert_ValidationException(int? id, int? layoutId, string name, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "was not recognized as a valid DateTime";

                // act
                var ex = Assert.Throws<FormatException>(
                                () => _evntService.Insert(new Event(id: id, layoutId: layoutId, name: name, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(1, 2, "First evnt of second layout", "jjl")]
        [TestCase(2, 1, "First evnt of first layout", "fhkh")]
        [TestCase(3, 2, "First evnt of second layout", "gjlgj")]
        public void EventService_Update_ValidationException(int? id, int? layoutId, string name, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "was not recognized as a valid DateTime";

                // act
                var ex = Assert.Throws<FormatException>(
                                () => _evntService.Update(new Event(id: id, layoutId: layoutId, name: name, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(2, 2, "First evnt of second layout", "fjfjfghjfg")]
        [TestCase(1, 1, "First evnt of first layout", "ghjfghjfghj")]
        public void EventService_Delete_ByEvent_Exc_refTable(int? id, int? layoutId, string name, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "dbo.Entity haven't this record of entity!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Delete(new Event(id: id, layoutId: layoutId, name: name, description: description)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(23525, 8, "Figngngndgndgnout", "hkfhjkghj")]
        public void EventService_Delete_ByEvent_Exc_HaventEvent(int? id, int? layoutId, string name, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have events to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _evntService.Delete(new Event(id: id, layoutId: layoutId, name: name, description: description)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventService_GetById_Exc_noEvent(int id)
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

        [Test]
        public void EventService_GetAll_Exc_noEvent()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _evntService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
