using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private ILayoutRepository _layoutRepository;

        [SetUp]
        public void Setup()
        {
            _layoutRepository = new LayoutRepository();
        }

        [TestCase(1, "First layout", 1, "description first layout")]
        [TestCase(2, "Second layout", 1, "description second layout")]
        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Insert_WhenInsertLayout_ShouldInt1(int id, string name, int venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _layoutRepository.Insert(new Layout(id: id, name: name, venueId: venueId, description: description));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, "First layout", 1, "description first layout")]
        [TestCase(2, "Second layout", 1, "description second layout")]
        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Update_WhenUpdateLayout_ShouldInt1(int id, string name, int venueId, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var expectedResponse = 1;

                // act
                var actualResponse = _layoutRepository.Update(new Layout(id: id, name: name, venueId: venueId, description: description));

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
                                () => _layoutRepository.Delete(id));

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
            var actualCount = _layoutRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _layoutRepository.GetById(1);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }

        [Test]
        public void GetAllByVenueId_WhenHave2Entry_Should2Entry()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = _layoutRepository.GetAllByVenueId(1).ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }
    }
}
