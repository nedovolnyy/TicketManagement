using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class SeatRepositoryTests
    {
        private readonly ISeatRepository _seatRepository = new SeatRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public void Insert_WhenInsertSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatRepository.Insert(new Seat(0, 1, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatRepository.Update(new Seat(3, 2, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _seatRepository.Delete(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _seatRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 2;

            // act
            var actualId = _seatRepository.GetById(2);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByAreaId_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _seatRepository.GetAllByAreaId(1).ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }
    }
}
