using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class LayoutServiceTests
    {
        private readonly LayoutService _layoutService = new LayoutService(new LayoutRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _layoutService.Insert(new Layout(0, "First egdfslayout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _layoutService.Update(new Layout(3, "Second ladfsgsdfyout", 2, "description second layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
            "An error occurred while saving the entity changes. See the inner exception for details.";

            // act
            var actualException = Assert.ThrowsAsync<DbUpdateException>(
                            async () => await _layoutService.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _layoutService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _layoutService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
