using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatServiceTests
    {
        private readonly ISeatService _seatService = TestDatabaseFixture.ServiceProvider.GetRequiredService<ISeatService>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _seatService.InsertAsync(new Seat(0, 2, 6, 5));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldUpdatedSeat()
        {
            // arrange
            var expectedSeat = new Seat(5, 5, 3, 5);

            // act
            await _seatService.UpdateAsync(expectedSeat);
            var actualResponse = await _seatService.GetByIdAsync(expectedSeat.Id);

            // assert
            Assert.AreEqual(expectedSeat, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _seatService.DeleteAsync(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _seatService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _seatService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
