using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class EventAreaServiceTests
    {
        private readonly EventAreaService _eventAreaService = new EventAreaService(new EventAreaRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public async Task Insert_WhenInsertEventArea_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _eventAreaService.Insert(new EventArea(0, 2, "Cinema Hall #1", 2, 1, 8.20m));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _eventAreaService.Update(new EventArea(2, 1, "Cinema Hall #2", 2, 1, 5.20m));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
            "An error occurred while saving the entity changes. See the inner exception for details.";

            // act
            var actualException = Assert.ThrowsAsync<DbUpdateException>(
                            async () => await _eventAreaService.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventAreaService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _eventAreaService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
