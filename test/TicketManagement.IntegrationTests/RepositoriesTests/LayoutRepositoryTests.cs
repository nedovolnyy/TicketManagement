using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DI;

namespace TicketManagement.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private readonly ILayoutRepository _layoutRepository = TestDatabaseFixture.Configuration.Container.GetInstance<ILayoutRepository>();

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _layoutRepository.Insert(new Layout(0, "First layout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _layoutRepository.Update(new Layout(3, "Second layout", 2, "description second layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteLayout_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _layoutRepository.Delete(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _layoutRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _layoutRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public async Task GetAllByVenueId_WhenHave2Entry_Should2Entry()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = (await _layoutRepository.GetAllByVenueId(1)).Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
