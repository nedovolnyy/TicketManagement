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

        [Test]
        public void Insert_WhenInsertLayout_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutService.Insert(new Layout(0, "First egdfslayout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateLayout_ShouldInt1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = _layoutService.Update(new Layout(3, "Second ladfsgsdfyout", 2, "description second layout"));

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
                            () => _layoutService.Delete(1));

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
