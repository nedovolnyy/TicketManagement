using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class AreaRepositoryTests
    {
        private readonly IAreaRepository _areaRepository = TestDatabaseFixture.Configuration.Container.GetInstance<IAreaRepository>();

        [Test]
        public async Task Insert_WhenInsertArea_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _areaRepository.Insert(new Area(0, 2, "First area of second layout", 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldUpdatedArea()
        {
            // arrange
            var expectedArea = new Area(1, 1, "Firs456547t etter of 3ett layout", 1, 7);

            // act
            await _areaRepository.Update(expectedArea);
            var actualResponse = await _areaRepository.GetById(expectedArea.Id);

            // assert
            Assert.AreEqual(expectedArea, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteArea_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _areaRepository.Delete(12);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _areaRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public async Task GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaRepository.GetAllByLayoutId(1)).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }
    }
}
