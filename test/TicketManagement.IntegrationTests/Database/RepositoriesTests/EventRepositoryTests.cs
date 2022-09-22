using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests.Database
{
    public class EventRepositoryTests
    {
        private static readonly IEventRepository _evntRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventRepository>();

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var expectedEventTime = DateTimeOffset.UtcNow.AddDays(2).ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            var expectedEndEventTime = DateTime.UtcNow.AddDays(2).AddMinutes(30).ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            var expectedEvent = new Event(0, "Stanger Things Serie", DateTimeOffset.Parse(expectedEventTime), "Stanger Things Serie", 1, DateTime.Parse(expectedEndEventTime), "image");

            // act
            await _evntRepository.InsertAsync(expectedEvent);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Events;

            // assert
            actualDbSet.Should().Contain(e => (e.EventTime == expectedEvent.EventTime) && (e.LayoutId == expectedEvent.LayoutId))
                .Which.Should().BeEquivalentTo(expectedEvent, opt => opt.Excluding(a => a.Path == "Id"));
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var expectedEventTime = DateTimeOffset.UtcNow.AddDays(2).ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            var expectedEndEventTime = DateTime.UtcNow.AddDays(2).AddMinutes(30).ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
            var expectedEvent = new Event(1, "Kitch45yen Serie", DateTimeOffset.Parse(expectedEventTime), "Kitcsdhen Serie", 1, DateTime.Parse(expectedEndEventTime), "image");

            // act
            await _evntRepository.UpdateAsync(expectedEvent);
            var actualEvent = await _evntRepository.GetByIdAsync(expectedEvent.Id);

            // assert
            actualEvent.Should().BeEquivalentTo(expectedEvent);
        }

        [Test]
        public async Task Delete_WhenDeleteEvent_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Events.Count() - 1;

            // act
            await _evntRepository.DeleteAsync(10);
            var actualCount = (await _evntRepository.GetAll().ToListAsyncSafe()).Count;

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameEvents()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Events;

            // act
            var actualCount = _evntRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventDbSet = TestDatabaseFixture.DatabaseContext.Events;

            // act
            var expectedEvent = await _evntRepository.GetByIdAsync(1);

            // assert
            actualEventDbSet.Should().ContainEquivalentOf(expectedEvent);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldContainThisEvents()
        {
            // arrange
            var actualEvents = TestDatabaseFixture.DatabaseContext.Events.ToList();

            // act
            var expectedEvents = _evntRepository.GetAllByLayoutId(1).ToList();

            // assert
            foreach (var evnt in expectedEvents)
            {
                actualEvents.Should().ContainEquivalentOf(evnt);
            }
        }
    }
}
