using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.IntegrationTests
{
    public class EventServiceTests
    {
        private readonly IEventService _eventService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventService>();

        [Test]
        public async Task GetSeatsAvaibleCount_WhenId2_ShouldIn1()
        {
            // arrange
            var expectedResponse = 1;

            // act
            var actualResponse = await _eventService.GetSeatsAvailableCountAsync(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldNotNull()
        {
            // act
            var actualResponse =
                await _eventService.InsertAsync(new Event(0, "Kitchegerrthrgn Serie", DateTimeOffset.Parse("07/02/2023"), "Kitchertrn Serie", 2, DateTime.Parse("2023-07-02 00:50:00"), "image"));

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldUpdatedEvent()
        {
            // arrange
            var expectedEvent = new Event(9, "StanegegerfgferThings Serie", DateTimeOffset.Parse("2023-11-06 00:45:00"), "Stanerger Thinegs Serie", 9, DateTime.Parse("2023-11-06 00:50:00"), "image");
            string expectedString =
                expectedEvent.Id.ToString() +
                expectedEvent.Name +
                expectedEvent.EventTime.ToString() +
                expectedEvent.Description +
                expectedEvent.LayoutId.ToString() +
                expectedEvent.EventEndTime.ToString();

            // act
            await _eventService.UpdateAsync(expectedEvent);
            var actualResponse = await _eventService.GetByIdAsync(expectedEvent.Id);

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
            var actualResponse = await _eventService.DeleteAsync(11);

            // assert
            Assert.NotNull(actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventService.GetAllAsync()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventService.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
