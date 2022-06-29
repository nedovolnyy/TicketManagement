using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatRepositoryTests
    {
        private readonly ISeatRepository _seatRepository = TestDatabaseFixture.Configuration.Container.GetInstance<ISeatRepository>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _seatRepository.Insert(new Seat(0, 1, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldUpdatedSeat()
        {
            // arrange
            var expectedSeat = new Seat(1, 1, 1, 1);

            // act
            await _seatRepository.Update(expectedSeat);
            var actualResponse = await _seatRepository.GetById(expectedSeat.Id);

            // assert
            Assert.AreEqual(expectedSeat, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _seatRepository.Delete(2);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _seatRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _seatRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public async Task GetAllByAreaId_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _seatRepository.GetAllByAreaId(1)).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }
    }
}
