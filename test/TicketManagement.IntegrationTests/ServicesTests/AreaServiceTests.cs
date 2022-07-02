using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class AreaServiceTests
    {
        private readonly IAreaService _areaService = TestDatabaseFixture.Configuration.Container.GetInstance<IAreaService>();

        [Test]
        public async Task Insert_WhenInsertArea_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _areaService.InsertAsync(new Area(0, 2, "First area of qwfqwef layout", 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldUpdatedArea()
        {
            // arrange
            var expectedArea = new Area(11, 2, "First etter of 3ett layout", 1, 7);

            // act
            await _areaService.UpdateAsync(expectedArea);
            var actualResponse = await _areaService.GetByIdAsync(expectedArea.Id);

            // assert
            Assert.AreEqual(expectedArea, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteArea_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _areaService.DeleteAsync(13);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _areaService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
