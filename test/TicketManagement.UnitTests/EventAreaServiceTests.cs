﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class EventAreaServiceTests
    {
        private readonly List<EventArea> _expectedEventAreas = new List<EventArea>
        {
            new EventArea(1, 2, "First eventArea of second layout", 2, 4, 5.8m),
            new EventArea(2, 1, "First eventArea of first layout", 3, 2, 8.6m),
            new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.6m),
        };

        [TestCase(1, 0, "First eventArea of second layout", 2, 4, 7.5)]
        [TestCase(2, 1, "", 3, 2, 5.5)]
        [TestCase(3, 2, "First eventArea of second layout", 0, 7, 4.3)]
        [TestCase(3, 2, "First eventArea of second layout", 1, 0, 4.3)]
        [TestCase(2, 1, "First eventArea of first layout", 3, 2, 0)]
        public void Validate_WhenEventAreaFieldNull_ShouldThrow(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var strException =
                "The field of EventArea is not allowed to be null!";
            var eventAreaExpected = new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "First eventArea of second layout", 2, 4, 7.5)]
        [TestCase(2, 1, "First eventArea of first layout", 3, 2, 5.5)]
        [TestCase(3, 2, "First eventArea of second layout", 1, 7, 4.3)]
        public void Insert_WhenInsertEventArea_ShouldNotNull(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var eventAreaExpected = new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Insert(It.IsAny<EventArea>())).Returns(1);
            var actual = eventAreaService.Object.Insert(eventAreaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(1, 2, "First eventArea of second layout", 2, 4, 7.5)]
        [TestCase(2, 1, "First eventArea of first layout", 3, 2, 5.5)]
        [TestCase(3, 2, "First eventArea of second layout", 1, 7, 4.3)]
        public void Update_WhenUpdateEventArea_ShouldNotNull(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var eventAreaExpected = new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Update(It.IsAny<EventArea>())).Returns(1);
            var actual = eventAreaService.Object.Update(eventAreaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenDeleteEventArea_ShouldNotNull(int id)
        {
            // arrange
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);
            var actual = eventAreaService.Object.Delete(id);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnEventAreaById_ShouldNotNull(int id)
        {
            // arrange
            var eventAreaExpected = new EventArea(id, 2, "First eventArea of first layout", 3, 2, 8.1m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventAreaExpected);
            var actual = eventAreaService.Object.GetById(id);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEventAreas_ShouldNotNull()
        {
            // arrange
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetAll()).Returns(_expectedEventAreas);
            var actual = eventAreaService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
