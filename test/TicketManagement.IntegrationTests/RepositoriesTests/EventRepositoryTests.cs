using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventRepositoryTests
    {
        private readonly IEventRepository _evntRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventRepository>();

        [Test]
        public async Task GetCountEmptySeats_WhenId2_ShouldNotNull()
        {
            // act
            var actualResponse = await _evntRepository.GetSeatsAvailableCountAsync(2);

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldNotNull()
        {
            // act
            var actualResponse =
                await _evntRepository.InsertAsync(
                    new Event(0, "Stanger Things Serie", DateTimeOffset.Parse("2023-09-19 00:05:00"), "Stanger Things Serie", 1, DateTime.Parse("2023-09-19 00:50:00"), "image"));

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldUpdatedEvent()
        {
            // arrange
            var expectedEvent = new Event(1, "Kitch45yen Serie", DateTimeOffset.Parse("2023-09-19 00:15:00"), "Kitcsdhen Serie", 1, DateTime.Parse("2023-09-09 00:50:00"), "image");
            string expectedString =
                expectedEvent.Id.ToString() +
                expectedEvent.Name +
                expectedEvent.EventTime.ToString() +
                expectedEvent.Description +
                expectedEvent.LayoutId.ToString() +
                expectedEvent.EventEndTime.ToString();

            // act
            await _evntRepository.UpdateAsync(expectedEvent);
            var actualResponse = await _evntRepository.GetByIdAsync(expectedEvent.Id);

            string actualString =
                actualResponse.Id.ToString() +
                actualResponse.Name +
                actualResponse.EventTime.ToString() +
                actualResponse.Description +
                actualResponse.LayoutId.ToString() +
                actualResponse.EventEndTime.ToString();

            // assert
            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldNotNull()
        {
            // act
            var actualResponse = await _evntRepository.DeleteAsync(10);

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _evntRepository.GetAll().AsEnumerable().Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _evntRepository.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // arrange
            var expectedCount = 1;

            // act
            var actualCount = _evntRepository.GetAllByLayoutId(1).AsEnumerable().Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
