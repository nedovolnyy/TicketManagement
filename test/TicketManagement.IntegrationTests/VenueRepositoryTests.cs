using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class VenueRepositoryTests
    {
        private readonly IVenueRepository _venueRepository = new VenueRepository(TestDatabaseFixture.DatabaseContext);

        [TestCase("Second venue", "description second venue", "address second venue", "+84845464")]
        public void Insert_WhenInsertVenue_ShouldInt1(string name, string description, string address, string phone)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueRepository.Insert(new Venue(0, name: name, description: description, address: address, phone: phone));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        public void Update_WhenUpdateVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _venueRepository.Update(new Venue(id: id, name: name, description: description, address: address, phone: phone));

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
                            () => _venueRepository.Delete(id));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _venueRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _venueRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
