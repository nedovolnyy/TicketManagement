using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class AreaServiceTests
    {
        private AreaService _areaService;

        [SetUp]
        public void Setup()
        {
            _areaService = new AreaService();
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void AreaService_Insert_ValidationException(int? id, int? layoutId, string description, int? coordX, int? coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Area description should be unique for area!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _areaService.Insert(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void AreaService_Update_ValidationException(int? id, int? layoutId, string description, int? coordX, int? coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Area description should be unique for area!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _areaService.Update(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(2, 2, "First area of second layout", 4, 6)]
        [TestCase(1, 1, "First area of first layout", 1, 1)]
        public void AreaService_Delete_ByArea_Exc_refTable(int? id, int? layoutId, string description, int? coordX, int? coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_Seat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.Seat\", column 'AreaId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _areaService.Delete(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(23525, 8, "Figngngndgndgnout", 4, 4)]
        public void AreaService_Delete_ByArea_Exc_HaventArea(int? id, int? layoutId, string description, int? coordX, int? coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have areas to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _areaService.Delete(new Area(id, layoutId, description, coordX, coordY)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void AreaService_Delete_ById_Exc_refTable(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_Seat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.Seat\", column 'AreaId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _areaService.Delete(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void AreaService_GetById_Exc_noArea(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have areas to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _areaService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void AreaService_GetAll_Exc_noArea()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _areaService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
