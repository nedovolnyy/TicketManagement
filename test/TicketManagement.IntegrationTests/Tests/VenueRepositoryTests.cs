using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class VenueRepositoryTests
    {
        private VenueRepository _venueRepository;

        [SetUp]
        public void Setup()
        {
            _venueRepository = new VenueRepository();
        }

        [Test]
        public void Venue_GetAll()
        {
            // act
            int exc = 3;

            // actual
            var venues = _venueRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(venues.Count, exc);
        }

        [Test]
        public void Venue_GetById()
        {
            // act
            int exc = 1;

            // actual
            var venue = _venueRepository.GetById(1);

            // assert
            Assert.AreEqual(venue.Id, exc);
        }
    }
}
