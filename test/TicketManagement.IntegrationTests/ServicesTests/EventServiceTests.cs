using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests
{
    public class EventServiceTests
    {
        private readonly EventService _eventService = new EventService(new EventRepository(TestDatabaseFixture.DatabaseContext));

        [Test]
        public async Task GetSeatsAvaibleCount_WhenId2_ShouldInt3()
        {
            // arrange
            var expectedResponse = 3;

            // act
            var actualResponse = await _eventService.GetSeatsAvailableCount(2);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public void Validate_WhenAreaHavntSeats_ShouldTrow()
        {
            // arrange
            var expectedException =
                "Create event is not possible! Haven't seats in Area!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                async () => await _eventService.Insert(
                    new Event(0, "Kitchegwcserrthrgn Serie", DateTimeOffset.Parse("2023-07-02 00:05:00"), "Kitschertrn Serie", 3, DateTime.Parse("2023-07-02 00:50:00"))));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public void Validate_WhenEventEndTimeLateEventTime_ShouldTrow()
        {
            // arrange
            var expectedException =
                "EventEndTime cannot be later than EventTime!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                async () => await _eventService.Insert(new Event(0, "Kitchegrgn Serie", DateTimeOffset.Parse("2023-01-01 00:50:00"), "Kitschertrn Serie", 3, DateTime.Parse("2023-01-01 00:45:00"))));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(expectedException));
        }

        [Test]
        public async Task Insert_WhenInsertEvent_ShouldInt15()
        {
            // arrange
            var expectedResponse = 15;

            // act
            var actualResponse =
                await _eventService.Insert(new Event(0, "Kitchegerrthrgn Serie", DateTimeOffset.Parse("07/02/2023"), "Kitchertrn Serie", 2, DateTime.Parse("2023-07-02 00:50:00")));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Update_WhenUpdateEvent_ShouldInt11()
        {
            // arrange
            var expectedResponse = 11;

            // act
            var actualResponse =
                await _eventService.Update(new Event(2, "StanegegererThings Serie", DateTimeOffset.Parse("06/11/2023"), "Stanerger Things Serie", 1, DateTime.Parse("2023-11-06 00:50:00")));

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task Delete_WhenDeleteSeat_ShouldInt7()
        {
            // arrange
            var expectedResponse = 7;

            // act
            var actualResponse = await _eventService.Delete(3);

            // assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [Test]
        public async Task GetAll_WhenHaveEntry_ShouldNotNull()
        {
            // act
            var actualCount = (await _eventService.GetAll()).Count();

            // assert
            Assert.IsNotNull(actualCount);
        }

        [Test]
        public async Task GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            var expectedId = 2;

            // act
            var actualId = await _eventService.GetById(2);

            // assert
            Assert.AreEqual(expectedId, actualId.Id);
        }
    }
}
