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
        public void SeatService_Insert_ValidationException(int? id, int? areaId, int? row, int? number)
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
        public void SeatService_Update_ValidationException(int? id, int? areaId, int? row, int? number)
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

        [TestCase(1, 5, 34, 4)]
        [TestCase(1, 4, 76, 4)]
        public void SeatService_Delete_BySeat_Exc_refTable(int? id, int? areaId, int? row, int? number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "dbo.Entity haven't this record of entity!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _seatService.Delete(new Seat(id: id, areaId: areaId, row: row, number: number)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(23525, 8, 43, 4)]
        public void SeatService_Delete_BySeat_Exc_HaventSeat(int? id, int? areaId, int? row, int? number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have seats to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _seatService.Delete(new Seat(id, areaId, row, number)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void SeatService_GetById_Exc_noSeat(int id)
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

        [Test]
        public void SeatService_GetAll_Exc_noSeat()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _seatService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
