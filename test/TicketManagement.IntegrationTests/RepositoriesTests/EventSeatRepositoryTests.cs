using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatRepositoryTests
    {
        private static readonly IEventSeatRepository _eventSeatRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventSeatRepository>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldBeEqualSameEventSeat()
        {
            // arrange
            var expectedEventSeat = new EventSeat(0, 2, 9, 1, true);

            // act
            await _eventSeatRepository.InsertAsync(expectedEventSeat);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.EventSeats;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedEventSeat, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldBeEqualSameEventSeat()
        {
            // arrange
            var upgradeEventSeat = new EventSeat(1, 1, 3, 3, true);
            var expectedEventSeat = await _eventSeatRepository.GetByIdAsync(upgradeEventSeat.Id);

            // act
            await _eventSeatRepository.UpdateAsync(expectedEventSeat);
            var actualEventSeat = await _eventSeatRepository.GetByIdAsync(upgradeEventSeat.Id);

            // assert
            actualEventSeat.Should().BeEquivalentTo(expectedEventSeat);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventSeats.Count() - 1;

            // act
            await _eventSeatRepository.DeleteAsync(2);
            var actualCount = _eventSeatRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameEventSeats()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventSeats;

            // act
            var actualCount = _eventSeatRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventSeatDbSet = TestDatabaseFixture.DatabaseContext.EventSeats;

            // act
            var expectedEventSeat = await _eventSeatRepository.GetByIdAsync(1);

            // assert
            actualEventSeatDbSet.Should().ContainEquivalentOf(expectedEventSeat);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldContainThisEventSeats()
        {
            // arrange
            var actualEventSeats = TestDatabaseFixture.DatabaseContext.EventSeats.ToList();

            // act
            var expectedEventSeats = _eventSeatRepository.GetAllByEventAreaId(1).ToList();

            // assert
            foreach (var eventSeat in expectedEventSeats)
            {
                actualEventSeats.Should().ContainEquivalentOf(eventSeat);
            }
        }
    }
}
