using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatServiceTests
    {
        private readonly ISeatService _seatService = TestDatabaseFixture.Configuration.Container.GetInstance<ISeatService>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _seatService.Insert(new Seat(0, 2, 6, 5));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _seatService.Update(new Seat(5, 2, 3, 5));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _seatService.Delete(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _seatService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = await _seatService.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
