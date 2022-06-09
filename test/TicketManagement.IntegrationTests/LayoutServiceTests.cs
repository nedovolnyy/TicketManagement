using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class LayoutServiceTests
    {
        private readonly LayoutService _layoutService = new LayoutService(new LayoutRepository(TestDatabaseFixture.DatabaseContext));

        [TestCase("First egdfslayout", 1, "description first layout")]
        public void Insert_WhenInsertLayout_ShouldInt1(string name, int venueId, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutService.Insert(new Layout(0, name: name, venueId: venueId, description: description));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(3, "Second ladfsgsdfyout", 2, "description second layout")]
        public void Update_WhenUpdateLayout_ShouldInt1(int id, string name, int venueId, string description)
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutService.Update(new Layout(id: id, name: name, venueId: venueId, description: description));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
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

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _layoutService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = _layoutService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
