using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class AreaRepositoryTests
    {
        private readonly IAreaRepository _areaRepository = new AreaRepository(TestDatabaseFixture.DatabaseContext);

        [TestCase(2, "First area of second layout", 1, 7)]
        public void Insert_WhenInsertArea_ShouldInt1(int layoutId, string description, int coordX, int coordY)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _areaRepository.Insert(new Area(0, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        public void Update_WhenUpdateArea_ShouldInt1(int id, int layoutId, string description, int coordX, int coordY)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _areaRepository.Update(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            // arrange
            var expectedException =
            "The DELETE statement conflicted with the REFERENCE constraint \"FK_Area_Seat\". " +
            "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.Seat\", column 'AreaId'.\r\n" +
            "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _areaRepository.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _areaRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _areaRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _areaRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }
    }
}
