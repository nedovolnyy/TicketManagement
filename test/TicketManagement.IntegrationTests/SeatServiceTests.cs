using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class SeatServiceTests
    {
        private readonly SeatService _seatService = new SeatService(new SeatRepository(TestDatabaseFixture.DatabaseContext));

        [TestCase(2, 6, 5)]
        public void Insert_WhenInsertSeat_ShouldInt1(int areaId, int row, int number)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Insert(new Seat(0, areaId: areaId, row: row, number: number));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(5, 2, 3, 5)]
        public void Update_WhenUpdateSeat_ShouldInt1(int id, int areaId, int row, int number)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Update(new Seat(id: id, areaId: areaId, row: row, number: number));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(3)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Delete(id);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _seatService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 5;

            // act
            var actualId = _seatService.GetById(5);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
