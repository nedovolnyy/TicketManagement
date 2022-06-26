﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private readonly ILayoutRepository _layoutRepository = new LayoutRepository(TestDatabaseFixture.DatabaseContext);

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
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedException =
            "An error occurred while saving the entity changes. See the inner exception for details.";

            // act
            var actualException = Assert.ThrowsAsync<DbUpdateException>(
                            async () => await _layoutRepository.Delete(1));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
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
