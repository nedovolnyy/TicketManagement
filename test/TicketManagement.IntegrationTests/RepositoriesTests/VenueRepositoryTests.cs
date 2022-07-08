using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class VenueRepositoryTests
    {
        private static readonly IVenueRepository _venueRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IVenueRepository>();

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldBeEqualSameVenue()
        {
            // arrange
            var expectedVenue = new Venue(0, "Seceond venfue", "description second venue", "address second venue", "+84845464");

            // act
            await _venueRepository.InsertAsync(expectedVenue);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Venues;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedVenue, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldBeEqualSameVenue()
        {
            // arrange
            var expectedVenue = new Venue(1, "Fjirst veejenue", "description first venue", "address first venue", "+4988955568");

            // act
            await _venueRepository.UpdateAsync(expectedVenue);
            var actualVenue = await _venueRepository.GetByIdAsync(expectedVenue.Id);

            // assert
            actualVenue.Should().BeEquivalentTo(expectedVenue);
        }

        [Test]
        public async Task Delete_WhenDeleteVenue_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Venues.Count() - 1;

            // act
            await _venueRepository.DeleteAsync(11);
            var actualCount = _venueRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameVenues()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Venues;

            // act
            var actualCount = _venueRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualVenueDbSet = TestDatabaseFixture.DatabaseContext.Venues;

            // act
            var expectedVenue = await _venueRepository.GetByIdAsync(1);

            // assert
            actualVenueDbSet.Should().ContainEquivalentOf(expectedVenue);
        }
    }
}
