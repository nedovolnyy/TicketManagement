using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class EventServiceTests
    {
        private readonly List<Event> _expectedEvents = new List<Event>
        {
            new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2),
            new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("2022-09-09 00:00:00.0000000 +03:00"), "Stanger Things Serie", 1),
        };

        [TestCase(2, 0, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie")]
        [TestCase(1, 2, "", "09/09/2021", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "0001-01-01 12:00:00.0000000 +12:00", "Things Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "")]
        public void Validate_WhenEventFieldNull_ShouldThrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "The field of Event is not allowed to be null!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2021", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie")]
        public void Validate_WhenEventTimeInPast_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Event can't be created in the past!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(2, 1, "Stanger Things Serie", "2022-09-09 00:00:00.0000000 +03:00", "Things Serie")]
        public void Validate_WhenEventInSameTimeForLayout_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Do not create event for the same layout in the same time!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Layout name should be unique in venue!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Insert_WhenInsertEvent_ShouldNotNull(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var eventExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };

            // act
            eventService.Setup(x => x.Insert(It.IsAny<Event>())).Returns(1);
            var actual = eventService.Object.Insert(eventExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Update_WhenUpdateEvent_ShouldNotNull(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            var eventExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };

            // act
            eventService.Setup(x => x.Update(It.IsAny<Event>())).Returns(1);
            var actual = eventService.Object.Update(eventExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenDeleteEvent_ShouldNotNull(int id)
        {
            // arrange
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };

            // act
            eventService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);
            var actual = eventService.Object.Delete(id);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnEventById_ShouldNotNull(int id)
        {
            // arrange
            var eventExpected = new Event(id, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2);
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };

            // act
            eventService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventExpected);
            var actual = eventService.Object.GetById(id);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEvents_ShouldNotNull()
        {
            // arrange
            var eventRepository = new Mock<IEventRepository> { CallBase = true };
            var eventService = new Mock<EventService>(eventRepository.Object) { CallBase = true };

            // act
            eventService.Setup(x => x.GetAll()).Returns(_expectedEvents);
            var actual = eventService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
