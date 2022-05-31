using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventSeatServiceTests
    {
        private EventSeatService _eventSeatService;

        [SetUp]
        public void Setup()
        {
            _eventSeatService = new EventSeatService();
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void EventSeatService_Insert_ValidationException(int? id, int? eventAreaId, int? row, int? number, int? state)
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
                                () => _eventSeatService.Insert(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(2, 6, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void EventSeatService_Update_ValidationException(int? id, int? eventAreaId, int? row, int? number, int? state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". "+
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n"+
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventSeatService.Update(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(1, 4, 1, 4, 0)]
        [TestCase(1, 1, 3, 4, 1)]
        public void EventSeatService_Delete_ByEventSeat_Exc_refTable(int? id, int? eventAreaId, int? row, int? number, int? state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "dbo.Entity haven't this record of entity!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventSeatService.Delete(new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(23525, 8, 43, 4, 4)]
        public void EventSeatService_Delete_ByEventSeat_Exc_HaventEventSeat(int? id, int? eventAreaId, int? row, int? number, int? state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have eventSeats to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventSeatService.Delete(new EventSeat(id, eventAreaId, row, number, state)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventSeatService_GetById_Exc_noEventSeat(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have eventSeats to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventSeatService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void EventSeatService_GetAll_Exc_noEventSeat()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventSeatService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
