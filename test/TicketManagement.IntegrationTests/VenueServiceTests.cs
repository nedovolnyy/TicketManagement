using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class VenueServiceTests
    {
        private VenueService _venueService;

        [SetUp]
        public void Setup()
        {
            _venueService = new VenueService(new VenueRepository());
        }

        [TestCase(1, "First wegwgvenue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second ergwergvenue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second vwergenue", "description second venue", "address second venue", "+84845464")]
        public void Insert_WhenInsertVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _venueService.Insert(new Venue(id: id, name: name, description: description, address: address, phone: phone));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, "First wergvenue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second wergvenue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second wrqvenue", "description second venue", "address second venue", "+84845464")]
        public void Update_WhenUpdateVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _venueService.Update(new Venue(id: id, name: name, description: description, address: address, phone: phone));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
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
        }

        [Test]
        public void GetAll_WhenHave3Entry_Should3Entry()
        {
            // arrange
            var expectedCount = 3;

            // act
            var actualCount = _venueService.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _venueService.GetById(1);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }
    }
}
