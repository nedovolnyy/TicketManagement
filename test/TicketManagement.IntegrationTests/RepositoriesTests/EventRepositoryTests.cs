﻿using System;
using System.Linq;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class EventRepositoryTests
    {
        private readonly IEventRepository _evntRepository = new EventRepository(TestDatabaseFixture.DatabaseContext);

        [Test]
        public void Insert_WhenInsertEvent_ShouldInt1()
        {
            // arrange
            var expectedResponse = 10;

            // act
            var actualResponse = _evntRepository.Insert(new Event(0, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2023"), "Stanger Things Serie", 1, DateTime.Parse("2023-09-19 00:50:00")));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Update_WhenUpdateEvent_ShouldInt1()
        {
            // arrange
            var expectedResponse = 23;

            // act
            var actualResponse = _evntRepository.Update(new Event(3, "Kitchen Serie", DateTimeOffset.Parse("09/09/2023"), "Kitchen Serie", 2, DateTime.Parse("2023-09-09 00:50:00")));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldInt1()
        {
            // arrange
            var expectedResponse = 8;

            // act
            var actualResponse = _evntRepository.Delete(1);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = _evntRepository.GetAll().ToList();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 3;

            // act
            var actualId = _evntRepository.GetById(3);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }

        [Test]
        public void GetAllByLayoutId_WhenHaveEntry_ShouldNotNull()
        {
            // arrange
            var expectedCount = 2;

            // act
            var actualCount = _evntRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(expectedCount, actualCount.Count);
        }
    }
}
