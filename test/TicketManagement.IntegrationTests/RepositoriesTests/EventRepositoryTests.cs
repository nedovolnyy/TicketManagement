using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventRepositoryTests
    {
        private static readonly IEventRepository _evntRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventRepository>();

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var expectedEvent = new Event(0, "Stanger Things Serie", DateTimeOffset.Parse("2023-09-19 00:05:00"), "Stanger Things Serie", 1, DateTime.Parse("2023-09-19 00:50:00"), "image");

            // act
            await _evntRepository.InsertAsync(expectedEvent);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Events;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedEvent, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldBeEqualSameEvent()
        {
            // arrange
            var upgradeEvent = new Event(1, "Kitch45yen Serie", DateTimeOffset.Parse("2023-09-19 00:15:00"), "Kitcsdhen Serie", 1, DateTime.Parse("2023-09-09 00:50:00"), "image");
            var expectedEvent = await _evntRepository.GetByIdAsync(upgradeEvent.Id);

            // act
            await _evntRepository.UpdateAsync(expectedEvent);
            var actualEvent = await _evntRepository.GetByIdAsync(upgradeEvent.Id);

            // assert
            actualEvent.Should().BeEquivalentTo(expectedEvent);
        }

        [Test]
        public async Task Delete_WhenDeleteEvent_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Events.Count();

            // act
            await _evntRepository.DeleteAsync(10);
            var actualCount = _evntRepository.GetAll().Count();

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
