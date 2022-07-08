using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class AreaRepositoryTests
    {
        private static readonly IAreaRepository _areaRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<IAreaRepository>();

        [Test]
        public async Task Insert_WhenInsertArea_ShouldBeEqualSameArea()
        {
            // arrange
            var expectedArea = new Area(0, 2, "Thousand area of second layout", 1, 7);

            // act
            await _areaRepository.InsertAsync(expectedArea);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Areas;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedArea, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldBeEqualSameArea()
        {
            // arrange
            var upgradeArea = new Area(1, 1, "Firs456547t etter of 3ett layout", 1, 7);
            var expectedArea = await _areaRepository.GetByIdAsync(upgradeArea.Id);

            // act
            await _areaRepository.UpdateAsync(expectedArea);
            var actualArea = await _areaRepository.GetByIdAsync(upgradeArea.Id);

            // assert
            actualArea.Should().BeEquivalentTo(expectedArea);
        }

        [Test]
        public async Task Delete_WhenDeleteArea_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Areas.Count() - 1;

            // act
            await _areaRepository.DeleteAsync(12);
            var actualCount = _areaRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameAreas()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Areas;

            // act
            var actualCount = _areaRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualAreaDbSet = TestDatabaseFixture.DatabaseContext.Areas;

            // act
            var expectedArea = await _areaRepository.GetByIdAsync(1);

            // assert
            actualAreaDbSet.Should().ContainEquivalentOf(expectedArea);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldContainThisAreas()
        {
            // arrange
            var actualAreas = TestDatabaseFixture.DatabaseContext.Areas.ToList();

            // act
            var expectedAreas = _areaRepository.GetAllByLayoutId(1).ToList();

            // assert
            foreach (var area in expectedAreas)
            {
                actualAreas.Should().ContainEquivalentOf(area);
            }
        }
    }
}
