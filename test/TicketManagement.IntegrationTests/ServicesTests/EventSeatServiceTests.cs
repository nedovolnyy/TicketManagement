using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatServiceTests
    {
        private readonly IEventSeatService _eventSeatService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventSeatService>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _eventSeatService.InsertAsync(new EventSeat(0, 3, 9, 1, true));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldUpdatedEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(4, 1, 3, 3, true);

            // act
            await _eventSeatService.UpdateAsync(expectedEventSeat);
            var actualResponse = await _eventSeatService.GetByIdAsync(expectedEventSeat.Id);

            // assert
            Assert.AreEqual(expectedEventSeat, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _eventSeatService.DeleteAsync(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventSeatService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventSeatService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
