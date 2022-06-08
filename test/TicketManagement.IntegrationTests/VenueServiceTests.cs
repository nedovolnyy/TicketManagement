using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class VenueServiceTests
    {
        private readonly VenueService _venueService = new VenueService(new VenueRepository(TestDatabaseFixture.DatabaseContext));

        [TestCase("Second vwergenue", "description second venue", "address second venue", "+84845464")]
        public void Insert_WhenInsertVenue_ShouldInt1(string name, string description, string address, string phone)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueService.Insert(new Venue(0, name: name, description: description, address: address, phone: phone));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1, "First wergvenue", "description first venue", "address first venue", "+4988955568")]
        public void Update_WhenUpdateVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueService.Update(new Venue(id: id, name: name, description: description, address: address, phone: phone));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            // arrange
            var expectedException =
                "The DELETE statement conflicted with the REFERENCE constraint \"FK_Venue_Layout\". " +
                "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.Layout\", column 'VenueId'.\r\n" +
                "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _venueService.Delete(id));

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
