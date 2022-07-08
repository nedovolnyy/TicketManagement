using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatServiceTests
    {
        private static readonly ISeatService _seatService = TestDatabaseFixture.ServiceProvider.GetRequiredService<ISeatService>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldBeEqualSameSeat()
        {
            // arrange
            var expectedSeat = new Seat(0, 2, 56, 19);

            // act
            await _seatService.InsertAsync(expectedSeat);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Seats;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedSeat, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldBeEqualSameSeat()
        {
            // arrange
            var upgradeSeat = new Seat(5, 2, 39, 15);
            var expectedSeat = await _seatService.GetByIdAsync(upgradeSeat.Id);

            // act
            await _seatService.UpdateAsync(expectedSeat);
            var actualSeat = await _seatService.GetByIdAsync(upgradeSeat.Id);

            // assert
            actualSeat.Should().BeEquivalentTo(expectedSeat);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Seats.Count() - 1;

            // act
            await _seatService.DeleteAsync(3);
            var actualCount = (await _seatService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameSeats()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Seats;

            // act
            var actualCount = await _seatService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualSeatDbSet = TestDatabaseFixture.DatabaseContext.Seats;

            // act
            var expectedSeat = await _seatService.GetByIdAsync(1);

            // assert
            actualSeatDbSet.Should().ContainEquivalentOf(expectedSeat);
        }

        [Test]
        public async Task GetAllByAreaId_WhenHaveEntry_ShouldContainThisSeats()
        {
            // arrange
            var actualSeats = TestDatabaseFixture.DatabaseContext.Seats.ToList();

            // act
            var expectedSeats = await _seatService.GetAllByAreaIdAsync(1);

            // assert
            foreach (var seat in expectedSeats)
            {
                actualSeats.Should().ContainEquivalentOf(seat);
            }
        }
    }
}
