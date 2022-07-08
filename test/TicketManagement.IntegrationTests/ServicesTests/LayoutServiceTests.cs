using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class LayoutServiceTests
    {
        private static readonly ILayoutService _layoutService = TestDatabaseFixture.ServiceProvider.GetRequiredService<ILayoutService>();

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldBeEqualSameLayout()
        {
            // arrange
            var expectedLayout = new Layout(0, "First egfyout", 1, "description first layout");

            // act
            await _layoutService.InsertAsync(expectedLayout);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Layouts;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedLayout, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldBeEqualSameLayout()
        {
            // arrange
            var expectedLayout = new Layout(1, "2", 1, "d2");

            // act
            await _layoutService.UpdateAsync(expectedLayout);
            var actualLayout = await _layoutService.GetByIdAsync(expectedLayout.Id);

            // assert
            actualLayout.Should().BeEquivalentTo(expectedLayout);
        }

        [Test]
        public async Task Delete_WhenDeleteLayout_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Layouts.Count() - 1;

            // act
            await _layoutService.DeleteAsync(8);
            var actualCount = (await _layoutService.GetAllAsync()).Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldSameLayouts()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Layouts;

            // act
            var actualCount = await _layoutService.GetAllAsync();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualLayoutDbSet = TestDatabaseFixture.DatabaseContext.Layouts;

            // act
            var expectedLayout = await _layoutService.GetByIdAsync(1);

            // assert
            actualLayoutDbSet.Should().ContainEquivalentOf(expectedLayout);
        }

        [Test]
        public async Task GetAllByVenueId_WhenHaveEntry_ShouldContainThisLayouts()
        {
            // arrange
            var actualLayouts = TestDatabaseFixture.DatabaseContext.Layouts.ToList();

            // act
            var expectedLayouts = await _layoutService.GetAllByVenueIdAsync(1);

            // assert
            foreach (var layout in expectedLayouts)
            {
                actualLayouts.Should().ContainEquivalentOf(layout);
            }
        }
    }
}
