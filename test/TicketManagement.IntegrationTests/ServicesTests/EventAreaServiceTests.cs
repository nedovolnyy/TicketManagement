using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventAreaServiceTests
    {
        private static readonly IEventAreaService _eventAreaService = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventAreaService>();

        [Test]
        public async Task Insert_WhenInsertEventArea_ShouldBeEqualSameEventArea()
        {
            // arrange
            var expectedEventArea = new EventArea(0, 2, "Cinema Hall #1", 2, 1, 8.20m);

            // act
            await _eventAreaService.InsertAsync(expectedEventArea);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.EventAreas;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedEventArea, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldBeEqualSameEventArea()
        {
            // arrange
            var expectedEventArea = new EventArea(2, 1, "Cinema Hall #2", 2, 1, 5.20m);

            // act
            await _eventAreaService.UpdateAsync(expectedEventArea);
            var actualEventArea = await _eventAreaService.GetByIdAsync(expectedEventArea.Id);

            // assert
            actualEventArea.Should().BeEquivalentTo(expectedEventArea);
        }

        [Test]
        public async Task Delete_WhenDeleteEventArea_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventAreas.Count() - 1;

            // act
            await _eventAreaService.DeleteAsync(10);
            var actualCount = (await _eventAreaService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameEventAreas()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventAreas;

            // act
            var actualCount = await _eventAreaService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventAreaDbSet = TestDatabaseFixture.DatabaseContext.EventAreas;

            // act
            var expectedEventArea = await _eventAreaService.GetByIdAsync(1);

            // assert
            actualEventAreaDbSet.Should().ContainEquivalentOf(expectedEventArea);
        }
    }
}
