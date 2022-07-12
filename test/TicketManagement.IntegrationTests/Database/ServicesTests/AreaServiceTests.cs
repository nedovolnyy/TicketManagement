using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests.Database
{
    public class AreaServicesTests
    {
        private static readonly IAreaService _areaServices = TestDatabaseFixture.ServiceProvider.GetRequiredService<IAreaService>();

        [Test]
        public async Task Insert_WhenInsertArea_ShouldBeEqualSameArea()
        {
            // arrange
            var areaDbSetBeforeInsert = await _areaServices.GetAllAsync();
            var expectedArea = new Area(0, 2, "Thousand area of second layout", 1, 7);

            // act
            await _areaServices.InsertAsync(expectedArea);
            var areaDbSetAfterInsert = await _areaServices.GetAllAsync();

            // assert
            areaDbSetBeforeInsert.Should().NotContainEquivalentOf(expectedArea, op => op.ExcludingMissingMembers());
            areaDbSetAfterInsert.Should().ContainEquivalentOf(expectedArea, op => op.ExcludingMissingMembers());
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
            var actualCount = (await _areaServices.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameAreas()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Areas;

            // act
            var actualCount = await _areaServices.GetAllAsync();

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
    }
}
