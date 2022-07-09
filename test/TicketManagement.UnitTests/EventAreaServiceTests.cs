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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventAreaService.Object.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventAreaService.Object.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventAreaService.Object.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventAreaService.Object.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await eventAreaService.Object.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertEventArea_ShouldNotNull()
        {
            // arrange
            var eventAreaExpected = new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.3m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };
            eventAreaService.Setup(x => x.InsertAsync(It.IsAny<EventArea>()));

            // act
            var actual = eventAreaService.Object.InsertAsync(eventAreaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task Update_WhenUpdateEventArea_ShouldNotNull()
        {
            // arrange
            int timesApplyRuleCalled = default;
            var eventAreaExpected = new EventArea(1, 2, "First eventArea of second layout", 2, 4, 7.5m);
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };
            eventAreaService.Setup(x => x.UpdateAsync(It.IsAny<EventArea>())).Callback(() => timesApplyRuleCalled++);

            // act
            await eventAreaService.Object.UpdateAsync(eventAreaExpected);

            // assert
            Assert.NotZero(timesApplyRuleCalled);
        }

        [Test]
        public void Delete_WhenDeleteEventArea_ShouldNotNull()
        {
            // arrange
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };
            eventAreaService.Setup(x => x.DeleteAsync(It.IsAny<int>()));

            // act
            var actual = eventAreaService.Object.DeleteAsync(1);

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
            eventAreaService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(eventAreaExpected);

            // act
            var actual = eventAreaService.Object.GetByIdAsync(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnEventAreas_ShouldNotNull()
        {
            // arrange
            var eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
            var eventAreaService = new Mock<EventAreaService>(eventAreaRepository.Object) { CallBase = true };
            eventAreaService.Setup(x => x.GetAllAsync()).ReturnsAsync(_expectedEventAreas);

            // act
            var actual = eventAreaService.Object.GetAllAsync();

            // assert
            Assert.NotNull(actual);
        }
    }
}
