using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private static readonly ILayoutRepository _layoutRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<ILayoutRepository>();

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldBeEqualSameLayout()
        {
            // arrange
            var expectedLayout = new Layout(0, "Fir5t lajyout", 1, "description first layout");

            // act
            await _layoutRepository.InsertAsync(expectedLayout);
            var actualDbSet = TestDatabaseFixture.DatabaseContext.Layouts;

            // assert
            actualDbSet.Should().ContainEquivalentOf(expectedLayout, op => op.ExcludingMissingMembers());
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldBeEqualSameLayout()
        {
            // arrange
            var expectedLayout = new Layout(2, "Sec0nd lahyout", 2, "description second layout");

            // act
            await _layoutRepository.UpdateAsync(expectedLayout);
            var actualLayout = await _layoutRepository.GetByIdAsync(expectedLayout.Id);

            // assert
            actualLayout.Should().BeEquivalentTo(expectedLayout);
        }

        [Test]
        public async Task Delete_WhenDeleteLayout_ShouldStateDeleted()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Layouts.Count() - 1;

            // act
            await _layoutRepository.DeleteAsync(7);
            var actualCount = _layoutRepository.GetAll().Count();

            // assert
            actualCount.Should().Be(expectedCount);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldSameLayouts()
        {
            // arrange
            var expectedCount = TestDatabaseFixture.DatabaseContext.Layouts;

            // act
            var actualCount = _layoutRepository.GetAll();

            // assert
            actualCount.Should().BeEquivalentTo(expectedCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var actualLayoutDbSet = TestDatabaseFixture.DatabaseContext.Layouts;

            // act
            var expectedLayout = await _layoutRepository.GetByIdAsync(1);

            // assert
            actualLayoutDbSet.Should().ContainEquivalentOf(expectedLayout);
        }

        [Test]
        public void GetAllByVenueId_WhenHaveEntry_ShouldContainThisLayouts()
        {
            // arrange
            var actualLayouts = TestDatabaseFixture.DatabaseContext.Layouts.ToList();

            // act
            var expectedLayouts = _layoutRepository.GetAllByVenueId(1).ToList();

            // assert
            foreach (var layout in expectedLayouts)
            {
                actualLayouts.Should().ContainEquivalentOf(layout);
            }
        }
    }
}
