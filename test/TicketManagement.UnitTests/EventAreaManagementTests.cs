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
    public class EventAreaManagementTests
    {
        private static readonly Mock<IEventAreaRepository> _eventAreaRepository = new Mock<IEventAreaRepository> { CallBase = true };
        private readonly EventAreaManagementController _eventManagementController = new EventAreaManagementController(_eventAreaRepository.Object);
        private readonly List<EventArea> _expectedEventAreas = new List<EventArea>
        {
            new EventArea(1, 2, "First eventArea of second layout", 2, 4, 5.8m),
            new EventArea(2, 1, "First eventArea of first layout", 3, 2, 8.6m),
            new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.6m),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _eventAreaRepository.Setup(x => x.InsertAsync(It.IsAny<EventArea>())).Callback(() => _timesApplyRuleCalled++);
            _eventAreaRepository.Setup(x => x.UpdateAsync(It.IsAny<EventArea>())).Callback(() => _timesApplyRuleCalled++);
            _eventAreaRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _eventAreaRepository.Setup(x => x.GetAll()).Returns(_expectedEventAreas.AsQueryable());
            foreach (var eventArea in _expectedEventAreas)
            {
                _eventAreaRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedEventAreas[eventArea.Id - 1]);
                _eventAreaRepository.Setup(x => x.GetAllByEventId(eventArea.EventId)).Returns(_expectedEventAreas.Where(x => x.EventId == eventArea.EventId).AsQueryable());
            }
        }

        [Test]
        public void Validate_WhenEventAreaFieldEventIdZero_ShouldThrow()
        {
            // arrange
            var eventAreaExpected = new EventArea
            {
                EventId = default,
                Description = _expectedEventAreas[0].Description,
                CoordX = _expectedEventAreas[0].CoordX,
                CoordY = _expectedEventAreas[0].CoordY,
                Price = _expectedEventAreas[0].Price,
            };
            var strException =
                "The field 'EventId' of EventArea is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventManagementController.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var eventAreaExpected = new EventArea
            {
                EventId = _expectedEventAreas[0].EventId,
                Description = string.Empty,
                CoordX = _expectedEventAreas[0].CoordX,
                CoordY = _expectedEventAreas[0].CoordY,
                Price = _expectedEventAreas[0].Price,
            };
            var strException =
                "The field 'Description' of EventArea is not allowed to be empty!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventManagementController.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldPriceZero_ShouldThrow()
        {
            // arrange
            var eventAreaExpected = new EventArea
            {
                EventId = _expectedEventAreas[0].EventId,
                Description = _expectedEventAreas[0].Description,
                CoordX = _expectedEventAreas[0].CoordX,
                CoordY = _expectedEventAreas[0].CoordY,
                Price = decimal.Zero,
            };
            var strException =
                "The field 'Price' of EventArea is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventManagementController.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldCoordXZero_ShouldThrow()
        {
            // arrange
            var eventAreaExpected = new EventArea
            {
                EventId = _expectedEventAreas[0].EventId,
                Description = _expectedEventAreas[0].Description,
                CoordX = default,
                CoordY = _expectedEventAreas[0].CoordY,
                Price = _expectedEventAreas[0].Price,
            };
            var strException =
                "The field 'CoordX' of EventArea is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventManagementController.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventAreaFieldCoordYZero_ShouldThrow()
        {
            // arrange
            var eventAreaExpected = new EventArea
            {
                EventId = _expectedEventAreas[0].EventId,
                Description = _expectedEventAreas[0].Description,
                CoordX = _expectedEventAreas[0].CoordX,
                CoordY = default,
                Price = _expectedEventAreas[0].Price,
            };
            var strException =
                "The field 'CoordY' of EventArea is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventManagementController.ValidateAsync(eventAreaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertEventArea_ShouldNotZeroCallback()
        {
            // arrange
            var eventAreaExpected = new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.3m);

            // act
            await _eventManagementController.InsertEventAreaAsync(eventAreaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateEventArea_ShouldNotZeroCallback()
        {
            // arrange
            var eventAreaExpected = new EventArea(1, 2, "First eventArea of second layout", 2, 4, 7.5m);

            // act
            await _eventManagementController.UpdateEventAreaAsync(eventAreaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteEventArea_ShouldNotZeroCallback()
        {
            // act
            await _eventManagementController.DeleteEventAreaAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnEventAreaById_ShouldNotNull()
        {
            // act
            var actual = await _eventManagementController.GetByIdEventAreaAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnEventAreas_ShouldNotZero()
        {
            // act
            var actual = (await _eventManagementController.GetAllEventAreasAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}