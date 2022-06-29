using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatRepositoryTests
    {
        private readonly ISeatRepository _seatRepository = TestDatabaseFixture.Configuration.Container.GetInstance<ISeatRepository>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _seatRepository.Insert(new Seat(0, 1, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _seatRepository.Update(new Seat(3, 2, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _seatRepository.Delete(12);

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
            var expectedId = 3;

            // act
            var actualId = await _seatRepository.GetById(3);

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
