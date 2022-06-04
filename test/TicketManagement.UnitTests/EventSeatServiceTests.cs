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
        public void EventSeatService_Insert_WhenFKConstraint_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
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
        public void EventSeatService_Update_WhenFKConstraint_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
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

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventSeatService_GetById_WhenNonExistentId_ShouldThrow(int id)
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
    }
}
