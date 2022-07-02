﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

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
        private int _timesApplyRuleCalled;

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
                            async () => await eventSeatService.Object.ValidateAsync(eventSeatExpected));

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
                            async () => await eventSeatService.Object.ValidateAsync(eventSeatExpected));

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
                            async () => await eventSeatService.Object.ValidateAsync(eventSeatExpected));

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
                            async () => await eventSeatService.Object.ValidateAsync(eventSeatExpected));

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
            eventSeatService.Setup(x => x.InsertAsync(It.IsAny<EventSeat>())).ReturnsAsync(1);

            // act
            var actual = eventSeatService.Object.InsertAsync(eventSeatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task Update_WhenUpdateEventSeat_ShouldNotNull()
        {
            // arrange
            var eventSeatExpected = new EventSeat(3, 5, 9, 1, 7);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.UpdateAsync(It.IsAny<EventSeat>())).Callback(() => _timesApplyRuleCalled++);

            // act
            await eventSeatService.Object.UpdateAsync(eventSeatExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [Test]
        public void Delete_WhenDeleteEventSeat_ShouldNotNull()
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(1);

            // act
            var actual = eventSeatService.Object.DeleteAsync(1);

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
            eventSeatService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(eventSeatExpected);

            // act
            var actual = eventSeatService.Object.GetByIdAsync(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEventSeats_ShouldNotNull()
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };
            eventSeatService.Setup(x => x.GetAllAsync()).ReturnsAsync(_expectedEventSeats);

            // act
            var actual = eventSeatService.Object.GetAllAsync();

            // assert
            Assert.NotNull(actual);
        }
    }
}
