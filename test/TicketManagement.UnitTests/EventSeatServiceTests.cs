using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class EventSeatServiceTests
    {
        private readonly List<EventSeat> _expectedEventSeats = new List<EventSeat>
        {
            new EventSeat(1, 6, 56, 2, 4),
            new EventSeat(2, 7, 3, 3, 2),
            new EventSeat(3, 5, 9, 1, 7),
        };

        [Test]
        public void Validate_WhenEventSeatFieldEventAreaIdNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'EventAreaId' of EventSeat is not allowed to be null!";
            var eventSeatExpected = new EventSeat(1, 0, 56, 2, 4);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventSeatService.Object.Validate(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventSeatFieldRowNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Row' of EventSeat is not allowed to be null!";
            var eventSeatExpected = new EventSeat(2, 7, 0, 3, 2);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventSeatService.Object.Validate(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventSeatFieldNumberNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Number' of EventSeat is not allowed to be null!";
            var eventSeatExpected = new EventSeat(3, 5, 9, 0, 7);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventSeatService.Object.Validate(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventSeatFieldStateNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'State' of EventSeat is not allowed to be null!";
            var eventSeatExpected = new EventSeat(1, 6, 56, 2, 0);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventSeatService.Object.Validate(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertEventSeat_ShouldNotNull()
        {
            // arrange
            var eventSeatExpected = new EventSeat(1, 6, 6, 2, 4);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.Insert(It.IsAny<EventSeat>())).ReturnsAsync(1);

            // act
            var actual = eventSeatService.Object.Insert(eventSeatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateEventSeat_ShouldNotNull()
        {
            // arrange
            var eventSeatExpected = new EventSeat(3, 5, 9, 1, 7);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.Update(It.IsAny<EventSeat>())).ReturnsAsync(1);

            // act
            var actual = eventSeatService.Object.Update(eventSeatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteEventSeat_ShouldNotNull()
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(1);

            // act
            var actual = eventSeatService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnEventSeatById_ShouldNotNull()
        {
            // arrange
            var eventSeatExpected = new EventSeat(5444, 6, 56, 2, 4);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(eventSeatExpected);

            // act
            var actual = eventSeatService.Object.GetById(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEventSeats_ShouldNotNull()
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.GetAll()).ReturnsAsync(_expectedEventSeats);

            // act
            var actual = eventSeatService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
