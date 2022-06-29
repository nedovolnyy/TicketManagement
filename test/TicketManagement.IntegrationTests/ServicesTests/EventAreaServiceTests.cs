using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventAreaServiceTests
    {
        private readonly IEventAreaService _eventAreaService = TestDatabaseFixture.Configuration.Container.GetInstance<IEventAreaService>();

        [Test]
        public async Task Insert_WhenInsertEventArea_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _eventAreaService.Insert(new EventArea(0, 2, "Cinema Hall #1", 2, 1, 8.20m));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _eventAreaService.Update(new EventArea(1, 1, "Cinema Hall #2", 2, 1, 5.20m));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventArea_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _eventAreaService.Delete(4);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventAreaService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventAreaService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
