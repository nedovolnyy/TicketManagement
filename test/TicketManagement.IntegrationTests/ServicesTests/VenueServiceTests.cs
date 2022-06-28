using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DI;

namespace TicketManagement.IntegrationTests
{
    public class VenueServiceTests
    {
        private readonly IVenueService _venueService = TestDatabaseFixture.Configuration.Container.GetInstance<IVenueService>();

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
        public async Task Delete_WhenDeleteVenue_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _venueService.Delete(2);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
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
            var expectedId = 3;

            // act
            var actualId = await _venueService.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
