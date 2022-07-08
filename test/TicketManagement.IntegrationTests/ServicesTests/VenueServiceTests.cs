using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class VenueServiceTests
    {
        private static readonly IVenueService _venueService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IVenueService>();

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldBeEqualSameVenue()
        {
            // arrange
            var expectedVenue = new Venue(0, "Score ven4ue", "description secodnd venue", "address secondd venue", "+84845464");

            // act
            await _venueService.InsertAsync(expectedVenue);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Venues;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedVenue, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldBeEqualSameVenue()
        {
            // arrange
            var expectedVenue = new Venue(5, "190th ve5nue", "description 4th venue", "address 4th venue", "+444444444444");

            // act
            await _venueService.UpdateAsync(expectedVenue);
            var actualVenue = await _venueService.GetByIdAsync(expectedVenue.Id);

            // assert
            actualVenue.Should().BeEquivalentTo(expectedVenue);
        }

        [Test]
        public async Task Delete_WhenDeleteVenue_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Venues.Count() - 1;

            // act
            await _venueService.DeleteAsync(12);
            var actualCount = (await _venueService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameVenues()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Venues;

            // act
            var actualCount = await _venueService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualVenueDbSet = TestDatabaseFixture.DatabaseContext.Venues;

            // act
            var expectedVenue = await _venueService.GetByIdAsync(1);

            // assert
            actualVenueDbSet.Should().ContainEquivalentOf(expectedVenue);
        }
    }
}
