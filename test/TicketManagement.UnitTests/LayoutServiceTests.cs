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
    public class LayoutServiceTests
    {
        private LayoutService _layoutService;

        [SetUp]
        public void Setup()
        {
            _layoutService = new LayoutService();
        }

        [TestCase(1, 1, "First layout")]
        [TestCase(2, 1, "Second layout")]
        [TestCase(3, 2, "Second layout")]
        public void LayoutService_Insert_ValidationException(int? id, int? venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Layout name should be unique in venue!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _layoutService.Insert(new Layout(id: id, venueId: venueId, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(1, 1, "First layout")]
        [TestCase(2, 1, "Second layout")]
        [TestCase(3, 2, "Second layout")]
        public void LayoutService_Update_ValidationException(int? id, int? venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                string strException =
                    "Layout name should be unique in venue!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _layoutService.Update(new Layout(id: id, venueId: venueId, description: description)));

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void LayoutService_GetById_Exc_noLayout(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have layouts to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _layoutService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void LayoutService_GetAll_Exc_noLayout()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _layoutService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
