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

        [TestCase(1, 0, 56, 2, 4)]
        [TestCase(2, 7, 0, 3, 2)]
        [TestCase(3, 5, 9, 0, 7)]
        [TestCase(1, 6, 56, 2, 0)]
        public void Validate_WhenEventSeatFieldNull_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            // arrange
            var strException =
                "The field of EventSeat is not allowed to be null!";
            var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventSeatService.Object.Validate(eventSeatExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 6, 6, 2, 4)]
        public void Insert_WhenInsertEventSeat_ShouldNotNull(int id, int eventAreaId, int row, int number, int state)
        {
            // arrange
            var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            eventSeatService.Setup(x => x.Insert(It.IsAny<EventSeat>())).Returns(1);
            var actual = eventSeatService.Object.Insert(eventSeatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(3, 5, 9, 1, 7)]
        public void Update_WhenUpdateEventSeat_ShouldNotNull(int id, int eventAreaId, int row, int number, int state)
        {
            // arrange
            var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            eventSeatService.Setup(x => x.Update(It.IsAny<EventSeat>())).Returns(1);
            var actual = eventSeatService.Object.Update(eventSeatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteEventSeat_ShouldNotNull(int id)
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            eventSeatService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);
            var actual = eventSeatService.Object.Delete(id);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(5444)]
        public void GetById_WhenReturnEventSeatById_ShouldNotNull(int id)
        {
            // arrange
            var eventSeatExpected = new EventSeat(id, 6, 56, 2, 4);
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            eventSeatService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventSeatExpected);
            var actual = eventSeatService.Object.GetById(id);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEventSeats_ShouldNotNull()
        {
            // arrange
            var eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
            var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

            // act
            eventSeatService.Setup(x => x.GetAll()).Returns(_expectedEventSeats);
            var actual = eventSeatService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
