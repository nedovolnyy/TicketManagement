using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventAreaRepositoryTests
    {
        private static readonly IEventAreaRepository _eventAreaRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IEventAreaRepository>();

        [Test]
        public async Task Insert_WhenInsertEventArea_ShouldBeEqualSameEventArea()
        {
            // arrange
            var expectedEventArea = new EventArea(0, 2, "Cinema Hall #1", 2, 1, 8.20m);

            // act
            await _eventAreaRepository.InsertAsync(expectedEventArea);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.EventAreas;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedEventArea, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldBeEqualSameEventArea()
        {
            // arrange
            var expectedEventArea = new EventArea(1, 1, "Cinema Hall #2", 2, 1, 5.20m);

            // act
            await _eventAreaRepository.UpdateAsync(expectedEventArea);
            var actualEventArea = await _eventAreaRepository.GetByIdAsync(expectedEventArea.Id);

            // assert
            actualEventArea.Should().BeEquivalentTo(expectedEventArea);
        }

        [Test]
        public async Task Delete_WhenDeleteEventArea_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventAreas.Count() - 1;

            // act
            await _eventAreaRepository.DeleteAsync(9);
            var actualCount = _eventAreaRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameEventAreas()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.EventAreas;

            // act
            var actualCount = _eventAreaRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualEventAreaDbSet = TestDatabaseFixture.DatabaseContext.EventAreas;

            // act
            var expectedEventArea = await _eventAreaRepository.GetByIdAsync(1);

            // assert
            actualEventAreaDbSet.Should().ContainEquivalentOf(expectedEventArea);
        }
    }
}
