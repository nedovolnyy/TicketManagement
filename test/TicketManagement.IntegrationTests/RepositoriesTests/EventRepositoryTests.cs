using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DI;

namespace TicketManagement.IntegrationTests
{
    public class EventRepositoryTests
    {
        private readonly IEventRepository _evntRepository = TestDatabaseFixture.Configuration.Container.GetInstance<IEventRepository>();

        [Test]
        public async Task GetCountEmptySeats_WhenId2_ShouldNotNull()
        {
            // act
            var actualResponse = await _evntRepository.GetSeatsAvailableCount(2);

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldNotNull()
        {
            // act
            var actualResponse =
                await _evntRepository.Insert(new Event(0, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2023"), "Stanger Things Serie", 1, DateTime.Parse("2023-09-19 00:50:00")));

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldNotNull()
        {
            // act
            var actualResponse = await _evntRepository.Update(new Event(3, "Kitchen Serie", DateTimeOffset.Parse("09/09/2023"), "Kitchen Serie", 2, DateTime.Parse("2023-09-09 00:50:00")));

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldNotNull()
        {
            // act
            var actualResponse = await _evntRepository.Delete(1);

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _evntRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = await _evntRepository.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public async Task GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = (await _evntRepository.GetAllByLayoutId(1)).Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
