using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class AreaRepositoryTests
    {
        private IAreaRepository _areaRepository;

        [SetUp]
        public void Setup()
        {
            _areaRepository = new AreaRepository();
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Insert_WhenInsertArea_ShouldInt1(int id, int layoutId, string description, int coordX, int coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _areaRepository.Insert(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Update_WhenUpdateArea_ShouldInt1(int id, int layoutId, string description, int coordX, int coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _areaRepository.Update(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

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
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_Seat\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.Seat\", column 'AreaId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _areaRepository.Delete(id));

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
            var areas = _areaRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(areas.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // act
            int exc = 1;

            // actual
            var area = _areaRepository.GetById(1);

            // assert
            Assert.AreEqual(area.Id, exc);
        }

        [Test]
        public void GetAllByLayoutId_WhenHave2Entry_Should2Entry()
        {
            // act
            int exc = 2;

            // actual
            var areas = _areaRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(areas.Count, exc);
        }
    }
}
