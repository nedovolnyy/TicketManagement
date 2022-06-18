using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class EventServiceTests
    {
        private readonly EventService _eventService = new EventService(new EventRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public void Insert_WhenInsertEvent_ShouldInt1()
        {
            // arrange
            var expectedResponse = 23;

            // act
            var actualResponse = _eventService.Insert(new Event(0, "Kitchegerrthrgn Serie", DateTimeOffset.Parse("07/02/2023"), "Kitchertrn Serie", 2));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateEvent_ShouldInt1()
        {
            // arrange
            var expectedResponse = 24;

            // act
            var actualResponse = _eventService.Update(new Event(3, "StanegegerergThings Serie", DateTimeOffset.Parse("06/11/2023"), "Stanerger Things Serie", 1));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 15;

            // act
            var actualResponse = _eventService.Delete(2);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _eventService.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = _eventService.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
