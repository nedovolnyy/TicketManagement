﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;

namespace TicketManagement.IntegrationTests
{
    public class LayoutRepositoryTests
    {
        private readonly ILayoutRepository _layoutRepository = TestDatabaseFixture.ServiceProvider.GetRequiredService<ILayoutRepository>();

        [Test]
        public async Task Insert_WhenInsertLayout_ShouldStateAdded()
        {
            // arrange
            var expectedResponse = (int)EntityState.Added;

            // act
            var actualResponse = await _layoutRepository.InsertAsync(new Layout(0, "First layout", 1, "description first layout"));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateLayout_ShouldUpdatedLayout()
        {
            // arrange
            var expectedLayout = new Layout(2, "Second layout", 2, "description second layout");

            // act
            await _layoutRepository.UpdateAsync(expectedLayout);
            var actualResponse = await _layoutRepository.GetByIdAsync(expectedLayout.Id);

            // assert
            Assert.AreEqual(expectedLayout, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteLayout_ShouldStateDeleted()
        {
            // arrange
            var expectedResponse = (int)EntityState.Deleted;

            // act
            var actualResponse = await _layoutRepository.DeleteAsync(7);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _layoutRepository.GetAll().Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 1;

            // act
            var actualId = await _layoutRepository.GetByIdAsync(1);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByVenueId_WhenHave2Entry_Should2Entry()
        {
            // arrange
            var expectedCount = 1;

            // act
            var actualCount = _layoutRepository.GetAllByVenueId(1).Count();

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
