using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventAreaServiceTests
    {
        private readonly IEventAreaService _eventAreaService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventAreaService>();

        [Test]
        public async Task Insert_WhenInsertEventArea_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _eventAreaService.InsertAsync(new EventArea(0, 2, "Cinema Hall #1", 2, 1, 8.20m));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldUpdatedEventArea()
        {
            // arrange
            var expectedEventArea = new EventArea(8, 8, "Cinema Hall #2", 2, 1, 5.20m);

            // act
            await _eventAreaService.UpdateAsync(expectedEventArea);
            var actualResponse = await _eventAreaService.GetByIdAsync(expectedEventArea.Id);

            // assert
            Assert.AreEqual(expectedEventArea, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventArea_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _eventAreaService.DeleteAsync(10);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventAreaService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventAreaService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
