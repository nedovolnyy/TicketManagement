using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class LayoutServiceTests
    {
        private LayoutService _layoutService;

        [SetUp]
        public void Setup()
        {
            _layoutService = new LayoutService(new LayoutRepository());
        }

        [TestCase(1, "First egdfslayout", 1, "description first layout")]
        [TestCase(2, "Second dfglayout", 1, "description second layout")]
        [TestCase(3, "Second ldfgayout", 2, "description second layout")]
        public void Insert_WhenInsertLayout_ShouldInt1(int id, string name, int venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _layoutService.Insert(new Layout(id: id, name: name, venueId: venueId, description: description));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, "First lsdgsdfgayout", 1, "description first layout")]
        [TestCase(2, "Second lsdfgsdfayout", 1, "description second layout")]
        [TestCase(3, "Second ladfsgsdfyout", 2, "description second layout")]
        public void Update_WhenUpdateLayout_ShouldInt1(int id, string name, int venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _layoutService.Update(new Layout(id: id, name: name, venueId: venueId, description: description));

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
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Layout_Area\". " +
                    "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.Area\", column 'LayoutId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var actualException = Assert.Throws<SqlException>(
                                () => _layoutService.Delete(id));

                // assert
                Assert.That(actualException.Message, Is.EqualTo(expectedException));
            }
        }

        [Test]
        public void GetAll_WhenHave7Entry_Should7Entry()
        {
            // arrange
            var expectedCount = 7;

            // act
            var actualCount = _layoutService.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _layoutService.GetById(1);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }
    }
}
