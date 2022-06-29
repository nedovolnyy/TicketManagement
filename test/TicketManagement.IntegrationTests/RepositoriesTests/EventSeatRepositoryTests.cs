using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatRepositoryTests
    {
        private readonly IEventSeatRepository _eventSeatRepository = TestDatabaseFixture.Configuration.Container.GetInstance<IEventSeatRepository>();

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _eventSeatRepository.Insert(new EventSeat(0, 2, 9, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _eventSeatRepository.Update(new EventSeat(7, 2, 3, 3, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteEventSeat_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _eventSeatRepository.Delete(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventSeatRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 10;

            // act
            var actualId = await _eventSeatRepository.GetById(10);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
