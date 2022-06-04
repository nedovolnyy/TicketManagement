using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class SeatServiceTests
    {
        private SeatService _seatService;

        [SetUp]
        public void Setup()
        {
            _seatService = new SeatService();
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(3, 2, 1, 1)]
        public void SeatService_Insert_WhenRowAndNumberNonUnique_ShouldThrow(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Row and number should be unique for area!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _seatService.Insert(new Seat(id: id, areaId: areaId, row: row, number: number)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(3, 2, 1, 1)]
        public void SeatService_Update_WhenRowAndNumberNonUnique_ShouldThrow(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Row and number should be unique for area!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _seatService.Update(new Seat(id: id, areaId: areaId, row: row, number: number)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void SeatService_GetById_WhenNonExistentId_ShouldThrow(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have seats to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _seatService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }
    }
}
