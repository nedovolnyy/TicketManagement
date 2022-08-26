using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.EventManagementAPI.Controllers;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class EventManagementTests
    {
        private static readonly Mock<IEventRepository> _eventRepository = new Mock<IEventRepository> { CallBase = true };
        private readonly EventManagementController _eventManagementController = new EventManagementController(_eventRepository.Object);
        private readonly List<Event> _expectedEvents = new List<Event>
        {
            new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2, DateTime.Parse("2022-09-09 00:50:00"), "image1"),
            new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("2022-09-09 00:00:00 +03:00"), "Stanger Things Serie", 1, DateTime.Parse("2022-09-09 00:50:00"), "image2"),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _eventRepository.Setup(x => x.InsertAsync(It.IsAny<Event>(), It.IsAny<decimal>())).Callback(() => _timesApplyRuleCalled++);
            _eventRepository.Setup(x => x.UpdateAsync(It.IsAny<Event>(), It.IsAny<decimal>())).Callback(() => _timesApplyRuleCalled++);
            _eventRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _eventRepository.Setup(x => x.GetAll()).Returns(_expectedEvents.AsQueryable());
            _eventRepository.Setup(x => x.GetSeatsAvailableCountAsync(It.IsAny<int>())).ReturnsAsync(1);
            _eventRepository.Setup(x => x.GetSeatsCountAsync(It.IsAny<int>())).ReturnsAsync((int)default);
            _eventRepository.Setup(x => x.GetSeatsCountAsync(It.IsIn(1))).ReturnsAsync(2);
            foreach (var evnt in _expectedEvents)
            {
                _eventRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedEvents[evnt.Id - 1]);
                _eventRepository.Setup(x => x.GetAllByLayoutId(evnt.LayoutId)).Returns(_expectedEvents.Where(x => x.LayoutId == evnt.LayoutId).AsQueryable());
            }
        }

        [Test]
        public async Task GetSeatsAvailableCount_When1_ShouldNotZero()
        {
            // act
            var actual = (await _eventManagementController.GetSeatsAvailableCountAsync(1)).Value;

            // assert
            Assert.NotZero(actual);
        }

        [Test]
        public void Validate_WhenAreaHavntSeats_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = _expectedEvents[0].Description,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = int.MaxValue,
            };
            var strException =
                "Create event is not possible! Haven't seats in Area!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldNameEmpty_ShouldThrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = string.Empty,
                Description = _expectedEvents[0].Description,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "The field 'Name' of Event is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = string.Empty,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "The field 'Description' of Event is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldLayoutIdZero_ShouldThrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = _expectedEvents[0].Description,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = default,
            };
            var strException =
                "The field 'LayoutId' of Event is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventFieldEventLogoImageEmpty_ShouldThrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = _expectedEvents[0].Description,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = string.Empty,
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "The field 'EventLogoImage' of Event is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventTimeInPast_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = _expectedEvents[0].Description,
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = DateTimeOffset.MinValue,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "Event can't be created in the past!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventEndTimeLateEventTime_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = _expectedEvents[0].Description,
                EventEndTime = DateTime.Now,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = _expectedEvents[0].EventLogoImage,
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "EventEndTime cannot be later than EventTime!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventInSameTimeForLayout_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = "any",
                Description = "any",
                EventEndTime = _expectedEvents[0].EventEndTime,
                EventTime = _expectedEvents[0].EventTime,
                EventLogoImage = "any",
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "Do not create event for the same layout in the same time!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventTimeLaterEventEndTime_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = "any",
                Description = "any",
                EventEndTime = DateTime.Now.AddHours(2),
                EventTime = DateTimeOffset.Now.AddHours(3),
                EventLogoImage = "any",
                LayoutId = int.MaxValue,
            };
            var strException =
                "EventEndTime cannot be later than EventTime!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow()
        {
            // arrange
            var eventExpected = new Event
            {
                Name = _expectedEvents[0].Name,
                Description = "any",
                EventEndTime = DateTime.Now.AddHours(3),
                EventTime = DateTimeOffset.Now.AddHours(2),
                EventLogoImage = "any",
                LayoutId = _expectedEvents[0].LayoutId,
            };
            var strException =
                "Layout name should be unique in venue!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _eventManagementController.ValidateAsync(eventExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertEvent_ShouldNotZeroCallback()
        {
            // arrange
            var eventExpected = new Event("Stanger Serie", DateTimeOffset.Parse("2022-09-19 00:05:00"), "Stanger Things Serie", 1, DateTime.Parse("2022-09-19 00:50:00"), "image");

            // act
            await _eventManagementController.InsertEventAsync(eventExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateEvent_ShouldNotZeroCallback()
        {
            // arrange
            var eventExpected = new Event(1, "Kitchen Serie", DateTimeOffset.Parse("2023-09-09 00:05:00"), "Kitchen Serie", 1, DateTime.Parse("2023-09-09 00:50:00"), "image");

            // act
            await _eventManagementController.UpdateEventAsync(eventExpected, decimal.One);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteEvent_ShouldNotZeroCallback()
        {
            // act
            await _eventManagementController.DeleteEventAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnEventById_ShouldNotNull()
        {
            // act
            var actual = await _eventManagementController.GetByIdEventAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnEvents_ShouldNotZero()
        {
            // act
            var actual = (await _eventManagementController.GetAllEventsAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}
