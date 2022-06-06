using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class VenueRepositoryTests
    {
        private IVenueRepository _venueRepository;

        [SetUp]
        public void Setup()
        {
            _venueRepository = new VenueRepository();
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Insert_WhenInsertVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _venueRepository.Insert(new Venue(id: id, name: name, description: description, address: address, phone: phone));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Update_WhenUpdateVenue_ShouldInt1(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _venueRepository.Update(new Venue(id: id, name: name, description: description, address: address, phone: phone));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenReferenceConstraint_ShouldThrowSqlException(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Venue_Layout\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.Layout\", column 'VenueId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _venueRepository.Delete(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void GetAll_WhenHave3Entry_Should3Entry()
        {
            // act
            int exc = 3;

            // actual
            var venues = _venueRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(venues.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
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
