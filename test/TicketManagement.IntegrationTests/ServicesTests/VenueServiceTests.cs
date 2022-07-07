using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.IntegrationTests
{
    public class VenueServiceTests
    {
        private readonly IVenueService _venueService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IVenueService>();

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _venueService.InsertAsync(new Venue(0, "Sixteen venue", "description secodnd venue", "address secondd venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldUpdatedVenue()
        {
            // arrange
            var expectedVenue = new Venue(4, "4th venue", "description 4th venue", "address 4th venue", "+444444444444");

            // act
            await _venueService.UpdateAsync(expectedVenue);
            var actualResponse = await _venueService.GetByIdAsync(expectedVenue.Id);

            // assert
            Assert.AreEqual(expectedVenue, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteVenue_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _venueService.DeleteAsync(12);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _venueService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = await _venueService.GetByIdAsync(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
