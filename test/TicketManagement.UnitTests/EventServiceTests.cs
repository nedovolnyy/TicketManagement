using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class EventServiceTests
    {
        private readonly List<Event> _expectedEvents = new List<Event>
        {
            new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2, DateTime.Parse("2022-09-09 00:50:00")),
            new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("2022-09-09 00:00:00 +03:00"), "Stanger Things Serie", 1, DateTime.Parse("2022-09-09 00:50:00")),
        };

        [Test]
        public void GetSeatsAvailableCount_When1_ShouldNotNull()
        {
            // arrange
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.GetSeatsAvailableCount(It.IsAny<int>())).ReturnsAsync(1);

            // act
            var actual = eventService.Object.GetSeatsAvailableCount(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Validate_WhenAreaHavntSeats_ShouldTrow()
        {
            // arrange
            var strException =
                "Create event is not possible! Haven't seats in Area!";
            var eventExpected = new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2022"), "Stanger Things Serie", 2, DateTime.Parse("2022-09-19 00:50:00"));
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.GetSeatsCount(It.IsAny<int>())).ReturnsAsync((int)default);

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventService.Object.Validate(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldNameEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Name' of Event is not allowed to be empty!";
            var evntExpected = new Event(2, "", DateTimeOffset.Parse("09/19/2022"), "Stanger Things Serie", 2, DateTime.Parse("2022-09-19 00:50:00"));
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Description' of Event is not allowed to be empty!";
            var evntExpected = new Event(1, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2022"), "", 2, DateTime.Parse("2022-09-19 00:50:00"));
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldLayoutIdNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'LayoutId' of Event is not allowed to be null!";
            var evntExpected = new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 0, DateTime.Parse("2022-09-09 00:50:00"));
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2021", "Kitchen Serie", "2021-09-09 00:50:00")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie", "2021-09-19 00:50:00")]
        public void Validate_WhenEventTimeInPast_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description, DateTime eventEndTime)
        {
            // arrange
            var strException =
                "Event can't be created in the past!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description, eventEndTime: eventEndTime);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).ReturnsAsync(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventEndTimeLateEventTime_ShouldTrow()
        {
            // arrange
            var strException =
                "EventEndTime cannot be later than EventTime!";
            var evntExpected = new Event(2, "Kitchegrgn Serie", DateTimeOffset.Parse("2023-01-01 00:50:00"), "Kitschertrn Serie", 2, DateTime.Parse("2023-01-01 00:45:00"));
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.Insert(evntExpected)).ReturnsAsync(1);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventInSameTimeForLayout_ShouldTrow()
        {
            // arrange
            var strException =
                "Do not create event for the same layout in the same time!";
            var evntExpected = new Event(2, "Stanweger Things Serie", DateTimeOffset.Parse("2022-09-09 00:00:00 +03:00"), "Things Serie", 1, DateTime.Parse("2022-09-09 00:50:00"));
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(1)).ReturnsAsync(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie", "2022-09-09 00:50:00")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie", "2022-09-19 00:50:00")]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description, DateTime eventEndTime)
        {
            // arrange
            var strException =
                "Layout name should be unique in venue!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description, eventEndTime: eventEndTime);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).ReturnsAsync(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertEvent_ShouldNotNull()
        {
            // arrange
            var eventExpected = new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2022"), "Stanger Things Serie", 1, DateTime.Parse("2022-09-19 00:50:00"));
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.Insert(It.IsAny<Event>())).ReturnsAsync(1);

            // act
            var actual = eventService.Object.Insert(eventExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateEvent_ShouldNotNull()
        {
            // arrange
            var eventExpected = new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2, DateTime.Parse("2022-09-09 00:50:00"));
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.Update(It.IsAny<Event>())).ReturnsAsync(1);

            // act
            var actual = eventService.Object.Update(eventExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteEvent_ShouldNotNull()
        {
            // arrange
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(1);

            // act
            var actual = eventService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnEventById_ShouldNotNull()
        {
            // arrange
            var eventExpected = new Event(5444, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2, DateTime.Parse("2022-09-09 00:50:00"));
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(eventExpected);

            // act
            var actual = eventService.Object.GetById(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEvents_ShouldNotNull()
        {
            // arrange
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };
            eventService.Setup(x => x.GetAll()).ReturnsAsync(_expectedEvents);

            // act
            var actual = eventService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
