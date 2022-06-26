using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class AreaServiceTests
    {
        private readonly IAreaService _areaService = new AreaService(new AreaRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public async Task Insert_WhenInsertArea_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _areaService.Insert(new Area(0, 2, "First area of qwfqwef layout", 1, 7));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _areaService.Update(new Area(3, 2, "First etter of 3ett layout", 1, 7));

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
                            async () => await _areaService.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _areaService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _areaService.GetById(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
