using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class VenueRepositoryTests
    {
        private readonly IVenueRepository _venueRepository = new VenueRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public async Task Insert_WhenInsertVenue_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _venueRepository.Insert(new Venue(0, "Second venue", "description second venue", "address second venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateVenue_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _venueRepository.Update(new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568"));

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
                            async () => await _venueRepository.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
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
            var expectedId = 1;

            // act
            var actualId = await _venueRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
