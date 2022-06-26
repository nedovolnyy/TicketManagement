﻿using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class EventSeatServiceTests
    {
        private readonly EventSeatService _eventSeatService = new EventSeatService(new EventSeatRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public async Task Insert_WhenInsertEventSeat_ShouldInt4()
        {
            // arrange
            var expectedResponse = 4;

            // act
            var actualResponse = await _eventSeatService.Insert(new EventSeat(0, 3, 9, 1, 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _eventSeatService.Update(new EventSeat(8, 2, 3, 3, 2));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldInt2()
        {
            // arrange
            var expectedResponse = 2;

            // act
            var actualResponse = await _eventSeatService.Delete(14);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventSeatService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 9;

            // act
            var actualId = await _eventSeatService.GetById(9);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
