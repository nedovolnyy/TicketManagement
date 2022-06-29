using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatServiceTests
    {
        private readonly IEventSeatService _eventSeatService = TestDatabaseFixture.Configuration.Container.GetInstance<IEventSeatService>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _eventSeatService.Insert(new EventSeat(0, 3, 9, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldUpdatedEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(4, 1, 3, 3, 2);

            // act
            await _eventSeatService.Update(expectedEventSeat);
            var actualResponse = await _eventSeatService.GetById(expectedEventSeat.Id);

            // assert
            Assert.AreEqual(expectedEventSeat, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _eventSeatService.Delete(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventSeatService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventSeatService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
