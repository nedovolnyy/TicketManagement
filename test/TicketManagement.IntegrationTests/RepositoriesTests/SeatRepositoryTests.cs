using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class SeatRepositoryTests
    {
        private static readonly ISeatRepository _seatRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<ISeatRepository>();

        [Test]
        public async Task Insert_WhenInsertSeat_ShouldBeEqualSameSeat()
        {
            // arrange
            var expectedSeat = new Seat(0, 1, 33, 39);

            // act
            await _seatRepository.InsertAsync(expectedSeat);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Seats;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedSeat, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateSeat_ShouldBeEqualSameSeat()
        {
            // arrange
            var expectedSeat = new Seat(1, 2, 55, 68);

            // act
            await _seatRepository.UpdateAsync(expectedSeat);
            var actualSeat = await _seatRepository.GetByIdAsync(expectedSeat.Id);

            // assert
            actualSeat.Should().BeEquivalentTo(expectedSeat);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Seats.Count() - 1;

            // act
            await _seatRepository.DeleteAsync(2);
            var actualCount = _seatRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameSeats()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Seats;

            // act
            var actualCount = _seatRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualSeatDbSet = TestDatabaseFixture.DatabaseContext.Seats;

            // act
            var expectedSeat = await _seatRepository.GetByIdAsync(1);

            // assert
            actualSeatDbSet.Should().ContainEquivalentOf(expectedSeat);
        }

        [Test]
        public void GetAllByAreaId_WhenHaveEntry_ShouldContainThisSeats()
        {
            // arrange
            var actualSeats = TestDatabaseFixture.DatabaseContext.Seats.ToList();

            // act
            var expectedSeats = _seatRepository.GetAllByAreaId(1).ToList();

            // assert
            foreach (var seat in expectedSeats)
            {
                actualSeats.Should().ContainEquivalentOf(seat);
            }
        }
    }
}
