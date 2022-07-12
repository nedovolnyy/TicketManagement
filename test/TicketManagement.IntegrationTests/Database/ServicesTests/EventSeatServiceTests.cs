using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests.Database
{
    public class EventSeatServiceTests
    {
        private static readonly IEventSeatService _eventSeatService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventSeatService>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldBeEqualSameEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(0, 3, 9, 1, 1);

            // act
            await _eventSeatService.InsertAsync(expectedEventSeat);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.EventSeats;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedEventSeat, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldBeEqualSameEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(4, 1, 3, 3, 1);

            // act
            await _eventSeatService.UpdateAsync(expectedEventSeat);
            var actualEventSeat = await _eventSeatService.GetByIdAsync(expectedEventSeat.Id);

            // assert
            actualEventSeat.Should().BeEquivalentTo(expectedEventSeat);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventSeats.Count() - 1;

            // act
            await _eventSeatService.DeleteAsync(3);
            var actualCount = (await _eventSeatService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameEventSeats()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventSeats;

            // act
            var actualCount = await _eventSeatService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventSeatDbSet = TestDatabaseFixture.DatabaseContext.EventSeats;

            // act
            var expectedEventSeat = await _eventSeatService.GetByIdAsync(1);

            // assert
            actualEventSeatDbSet.Should().ContainEquivalentOf(expectedEventSeat);
        }

        [Test]
        public async Task GetAllByEventAreaId_WhenHaveEntry_ShouldContainThisEventSeats()
        {
            // arrange
            var actualEventSeats = TestDatabaseFixture.DatabaseContext.EventSeats.ToList();

            // act
            var expectedEventSeats = await _eventSeatService.GetAllByEventAreaIdAsync(1);

            // assert
            foreach (var eventSeat in expectedEventSeats)
            {
                actualEventSeats.Should().ContainEquivalentOf(eventSeat);
            }
        }
    }
}
