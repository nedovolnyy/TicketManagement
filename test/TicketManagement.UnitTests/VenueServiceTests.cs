using System;
using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class VenueServiceTests
    {
        private VenueService _venueService;

        [SetUp]
        public void Setup()
        {
            _venueService = new VenueService();
        }

        [TestCase(1, "First venue", "dggdfd", "+4988955568")]
        [TestCase(2, "Second venue", "st DFgee", "+58487555")]
        [TestCase(3, "Second venue", "df eErtg", "+84845464")]
        public void VenueService_Insert_ValidationException(int? id, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "The Venue description has not unique!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _venueService.Insert(new Venue(id: id, description: description, address: address, phone: phone)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(1, "First venue", "dggdfd", "+4988955568")]
        [TestCase(2, "Second venue", "st DFgee", "+58487555")]
        [TestCase(3, "Second venue", "df eErtg", "+84845464")]
        public void VenueService_Update_ValidationException(int? id, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "The Venue description has not unique!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _venueService.Update(new Venue(id: id, description: description, address: address, phone: phone)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void VenueService_GetById_Exc_noVenue(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have venues to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _venueService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void VenueService_GetAll_Exc_noVenue()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _venueService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
