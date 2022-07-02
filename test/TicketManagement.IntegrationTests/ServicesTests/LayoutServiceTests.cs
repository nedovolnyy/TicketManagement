using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class LayoutServiceTests
    {
        private readonly ILayoutService _layoutService = TestDatabaseFixture.Configuration.Container.GetInstance<ILayoutService>();

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _layoutService.InsertAsync(new Layout(0, "First egdfslayout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldUpdatedLayout()
        {
            // arrange
            var expectedLayout = new Layout(1, "Second ladfsgsdfyout", 1, "description second layout");

            // act
            await _layoutService.UpdateAsync(expectedLayout);
            var actualResponse = await _layoutService.GetByIdAsync(expectedLayout.Id);

            // assert
            Assert.AreEqual(expectedLayout, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteLayout_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _layoutService.DeleteAsync(8);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _layoutService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _layoutService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
