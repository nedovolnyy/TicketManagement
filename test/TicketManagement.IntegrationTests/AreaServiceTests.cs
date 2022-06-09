using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class AreaServiceTests
    {
        private readonly IAreaService _areaService = new AreaService(new AreaRepository(TestDatabaseFixture.DatabaseContext));

        [TestCase(2, "First area of qwef layout", 2, 4)]
        public void Insert_WhenInsertArea_ShouldInt1(int layoutId, string description, int coordX, int coordY)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _areaService.Insert(new Area(0, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(3, 2, "First area of qwfqwef layout", 1, 7)]
        public void Update_WhenUpdateArea_ShouldInt1(int id, int layoutId, string description, int coordX, int coordY)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _areaService.Update(new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY));

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
                            () => _areaService.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _areaService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _areaService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
