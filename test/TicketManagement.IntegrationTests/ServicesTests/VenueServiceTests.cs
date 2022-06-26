using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class VenueServiceTests
    {
        private readonly VenueService _venueService = new VenueService(new VenueRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public void Validate_WhenNameNonUnique_ShouldTrow()
        {
            // arrange
            var expectedException =
                "The Venue name is not unique!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _venueService.Insert(new Venue(0, "Second venue", "description second venue", "address second venue", "+84845464")));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _venueService.Insert(new Venue(0, "Second vwergenue", "description second venue", "address second venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _venueService.Update(new Venue(1, "First wergvenue", "description first venue", "address first venue", "+4988955568"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
            "An error occurred while saving the entity changes. See the inner exception for details.";

            // act
            var actualException = Assert.ThrowsAsync<DbUpdateException>(
                            async () => await _venueService.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _venueService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _venueService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
