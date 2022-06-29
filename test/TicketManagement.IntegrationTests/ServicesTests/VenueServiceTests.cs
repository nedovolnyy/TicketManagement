using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

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
                            async () => await _venueService.Insert(new Venue(0, "Second venue", "description of second venue", "address second venue", "+84845464")));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _venueService.Insert(new Venue(0, "Sixteen venue", "description secodnd venue", "address secondd venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldUpdatedVenue()
        {
            // arrange
            var expectedVenue = new Venue(4, "4th venue", "description 4th venue", "address 4th venue", "+444444444444");

            // act
            await _venueService.Update(expectedVenue);
            var actualResponse = await _venueService.GetById(expectedVenue.Id);

            // assert
            Assert.AreEqual(expectedVenue, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteVenue_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _venueService.Delete(12);

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
