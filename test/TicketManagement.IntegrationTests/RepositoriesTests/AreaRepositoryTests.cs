using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class AreaRepositoryTests
    {
        private readonly IAreaRepository _areaRepository = new AreaRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public async Task Insert_WhenInsertArea_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _areaRepository.Insert(new Area(0, 2, "First area of second layout", 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _areaRepository.Update(new Area(1, 2, "First area of second layout", 2, 4));

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
                             async () => await _areaRepository.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaRepository.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _areaRepository.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public async Task GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaRepository.GetAllByLayoutId(1)).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }
    }
}
