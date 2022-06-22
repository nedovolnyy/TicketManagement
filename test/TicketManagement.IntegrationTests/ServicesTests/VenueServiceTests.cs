using System.Linq;
using Microsoft.Data.SqlClient;
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
            var actualException = Assert.Throws<ValidationException>(
                            () => _venueService.Insert(new Venue(0, "Second venue", "description second venue", "address second venue", "+84845464")));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void Insert_WhenInsertVenue_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueService.Insert(new Venue(0, "Second vwergenue", "description second venue", "address second venue", "+84845464"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateVenue_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueService.Update(new Venue(1, "First wergvenue", "description first venue", "address first venue", "+4988955568"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
                "The DELETE statement conflicted with the REFERENCE constraint \"FK_Venue_Layout\". " +
                "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.Layout\", column 'VenueId'.\r\n" +
                "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _venueService.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _venueService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _venueService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
