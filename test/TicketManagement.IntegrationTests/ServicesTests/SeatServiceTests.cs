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

        [Test]
        public void Insert_WhenInsertSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Insert(new Seat(0, 2, 6, 5));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Update(new Seat(5, 2, 3, 5));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatService.Delete(3);

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
