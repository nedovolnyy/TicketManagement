using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatRepositoryTests
    {
        private readonly IEventSeatRepository _eventSeatRepository = TestDatabaseFixture.Configuration.Container.GetInstance<IEventSeatRepository>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _eventSeatRepository.InsertAsync(new EventSeat(0, 2, 9, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldUpdatedEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(1, 1, 3, 3, 1);

            // act
            await _eventSeatRepository.UpdateAsync(expectedEventSeat);
            var actualResponse = await _eventSeatRepository.GetByIdAsync(expectedEventSeat.Id);

            // assert
            Assert.AreEqual(expectedEventSeat, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _eventSeatRepository.DeleteAsync(2);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventSeatRepository.GetAll().Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventSeatRepository.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
