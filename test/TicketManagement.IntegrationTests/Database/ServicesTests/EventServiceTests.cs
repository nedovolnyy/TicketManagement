using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests.Database
{
    public class EventServiceTests
    {
        private static readonly IEventService _evntService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventService>();

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var expectedEvent = new Event(0, "Kitchegerrthrgn Serie", DateTimeOffset.Parse("2023-07-03 00:35:00"), "Kitchertrn Serie", 5, DateTime.Parse("2023-07-03 00:50:00"), "image");

            // act
            await _evntService.InsertAsync(expectedEvent);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Events;

            // assert
            actualDbSet.Should().Contain(e => (e.EventTime == expectedEvent.EventTime) && (e.LayoutId == expectedEvent.LayoutId))
                .Which.Should().BeEquivalentTo(expectedEvent, opt => opt.Excluding(a => a.Path == "Id"));
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var expectedEvent = new Event(9, "StanegegerfgferThings Serie", DateTimeOffset.Parse("2023-11-06 00:45:00"), "Stanerger Thinegs Serie", 9, DateTime.Parse("2023-11-06 06:50:00"), "image");

            // act
            await _evntService.UpdateAsync(expectedEvent);
            var actualEvent = await _evntService.GetByIdAsync(expectedEvent.Id);

            // assert
            actualEvent.Should().BeEquivalentTo(expectedEvent);
        }

        [Test]
        public async Task Delete_WhenDeleteEvent_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Events.Count() - 1;

            // act
            await _evntService.DeleteAsync(11);
            var actualCount = (await _evntService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameEvents()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Events;

            // act
            var actualCount = await _evntService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventDbSet = TestDatabaseFixture.DatabaseContext.Events;

            // act
            var expectedEvent = await _evntService.GetByIdAsync(1);

            // assert
            actualEventDbSet.Should().ContainEquivalentOf(expectedEvent);
        }

        [Test]
        public async Task GetAllByEventAreaId_WhenHaveEntry_ShouldContainThisEventSeats()
        {
            // arrange
            var actualEventSeats = TestDatabaseFixture.DatabaseContext.Events.ToList();

            // act
            var expectedEventSeats = await _evntService.GetAllByLayoutIdAsync(1);

            // assert
            foreach (var eventSeat in expectedEventSeats)
            {
                actualEventSeats.Should().ContainEquivalentOf(eventSeat);
            }
        }
    }
}
