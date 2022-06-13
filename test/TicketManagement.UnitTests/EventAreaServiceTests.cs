using System.Collections.Generic;
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

        [Test]
        public void Validate_WhenEventAreaFieldLayoutIdNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'LayoutId' of EventArea is not allowed to be null!";
            var eventAreaExpected = new EventArea(1, 0, "First eventArea of second layout", 2, 4, 7.5m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Description' of EventArea is not allowed to be empty!";
            var eventAreaExpected = new EventArea(2, 1, "", 3, 2, 5.5m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldPriceNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Price' of EventArea is not allowed to be null!";
            var eventAreaExpected = new EventArea(3, 2, "First eventArea of second layout", 5, 7, 0);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldCoordXNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'CoordX' of EventArea is not allowed to be null!";
            var eventAreaExpected = new EventArea(3, 2, "First eventArea of second layout", 0, 1, 4.3m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldCoordYNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'CoordY' of EventArea is not allowed to be null!";
            var eventAreaExpected = new EventArea(2, 1, "First eventArea of first layout", 3, 0, 6.2m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => eventAreaService.Object.Validate(eventAreaExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertEventArea_ShouldNotNull()
        {
            // arrange
            var eventAreaExpected = new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.3m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Insert(It.IsAny<EventArea>())).Returns(1);
            var actual = eventAreaService.Object.Insert(eventAreaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateEventArea_ShouldNotNull()
        {
            // arrange
            var eventAreaExpected = new EventArea(1, 2, "First eventArea of second layout", 2, 4, 7.5m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Update(It.IsAny<EventArea>())).Returns(1);
            var actual = eventAreaService.Object.Update(eventAreaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteEventArea_ShouldNotNull()
        {
            // arrange
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);
            var actual = eventAreaService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnEventAreaById_ShouldNotNull()
        {
            // arrange
            var eventAreaExpected = new EventArea(5444, 2, "First eventArea of first layout", 3, 2, 8.1m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventAreaExpected);
            var actual = eventAreaService.Object.GetById(5444);

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
