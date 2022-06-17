using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private readonly ILayoutRepository _layoutRepository = new LayoutRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public void Insert_WhenInsertLayout_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutRepository.Insert(new Layout(0, "First layout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateLayout_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutRepository.Update(new Layout(3, "Second layout", 2, "description second layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
                "The DELETE statement conflicted with the REFERENCE constraint \"FK_Layout_Area\". " +
                "The conflict occurred in database \"TestTicketManagement.Database\", table \"dbo.Area\", column 'LayoutId'.\r\n" +
                "The statement has been terminated.";

            // act
            var actualException = Assert.Throws<SqlException>(
                            () => _layoutRepository.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _layoutRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _layoutRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByVenueId_WhenHave2Entry_Should2Entry()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = _layoutRepository.GetAllByVenueId(1).ToList();

            // assert
            Assert.AreEqual(expectedCount, actualCount.Count);
        }
    }
}
