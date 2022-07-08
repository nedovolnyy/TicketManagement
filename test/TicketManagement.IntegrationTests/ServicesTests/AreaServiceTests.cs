using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class AreaServicesTests
    {
        private static readonly IAreaRepository _areaServices = TestDatabaseFixture.ServiceProvider.GetRequiredService<IAreaRepository>();

        [Test]
        public async Task Insert_WhenInsertArea_ShouldBeEqualSameArea()
        {
            // arrange
            var expectedArea = new Area(0, 2, "Thousand area of second layout", 1, 7);

            // act
            await _areaServices.InsertAsync(expectedArea);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Areas;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedArea, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldBeEqualSameArea()
        {
            // arrange
            var expectedArea = new Area(2, 1, "Firs456547t etter of 3ett layout", 1, 7);

            // act
            await _areaServices.UpdateAsync(expectedArea);
            var actualArea = await _areaServices.GetByIdAsync(expectedArea.Id);

            // assert
            actualArea.Should().BeEquivalentTo(expectedArea);
        }

        [Test]
        public async Task Delete_WhenDeleteArea_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Areas.Count() - 1;

            // act
            await _areaServices.DeleteAsync(13);
            var actualCount = _areaServices.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameAreas()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Areas;

            // act
            var actualCount = _areaServices.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualAreaDbSet = TestDatabaseFixture.DatabaseContext.Areas;

            // act
            var expectedArea = await _areaServices.GetByIdAsync(1);

            // assert
            actualAreaDbSet.Should().ContainEquivalentOf(expectedArea);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldContainThisAreas()
        {
            // arrange
            var actualAreas = TestDatabaseFixture.DatabaseContext.Areas.ToList();

            // act
            var expectedAreas = _areaServices.GetAllByLayoutId(1).ToList();

            // assert
            foreach (var area in expectedAreas)
            {
                actualAreas.Should().ContainEquivalentOf(area);
            }
        }
    }
}
