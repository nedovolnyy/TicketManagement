using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
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
                int expected = 1;

                // act
                var actual = _layoutRepository.Insert(new Layout(id: id, name: name, venueId: venueId, description: description));

                // assert
                Assert.AreEqual(expected, actual);
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
                int expected = 1;

                // act
                var actual = _layoutRepository.Update(new Layout(id: id, name: name, venueId: venueId, description: description));

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
                    "The DELETE statement conflicted with the REFERENCE constraint \"FK_Layout_Area\". " +
                    "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.Area\", column 'LayoutId'.\r\n" +
                    "The statement has been terminated.";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _layoutRepository.Delete(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [Test]
        public void GetAll_WhenHave7Entry_Should7Entry()
        {
            // act
            int exc = 7;

            // actual
            var layouts = _layoutRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(layouts.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // act
            int exc = 1;

            // actual
            var layout = _layoutRepository.GetById(1);

            // assert
            Assert.AreEqual(layout.Id, exc);
        }

        [Test]
        public void GetAllByVenueId_WhenHave2Entry_Should2Entry()
        {
            // act
            int exc = 2;

            // actual
            var layouts = _layoutRepository.GetAllByVenueId(1).ToList();

            // assert
            Assert.AreEqual(layouts.Count, exc);
        }
    }
}
