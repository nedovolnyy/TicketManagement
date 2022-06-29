using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class VenueRepositoryTests
    {
        private readonly IVenueRepository _venueRepository = TestDatabaseFixture.Configuration.Container.GetInstance<IVenueRepository>();

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _venueRepository.Insert(new Venue(0, "Second venue", "description second venue", "address second venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldUpdatedVenue()
        {
            // arrange
            var expectedVenue = new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568");

            // act
            await _venueRepository.Update(expectedVenue);
            var actualResponse = await _venueRepository.GetById(expectedVenue.Id);

            // assert
            Assert.AreEqual(expectedVenue, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteVenue_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _venueRepository.Delete(11);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _venueRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 2;

            // act
            var actualId = await _venueRepository.GetById(2);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
