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
    public class EventSeatManagementTests
    {
        private static readonly Mock<IEventSeatRepository> _eventSeatRepository = new Mock<IEventSeatRepository> { CallBase = true };
        private readonly EventSeatManagementController _eventSeatManagementController = new EventSeatManagementController(_eventSeatRepository.Object);
        private readonly List<EventSeat> _expectedEventSeats = new List<EventSeat>
        {
            new EventSeat(1, 6, 56, 2, State.NotAvailable),
            new EventSeat(2, 7, 3, 3, State.Available),
            new EventSeat(3, 5, 9, 1, State.NotAvailable),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _eventSeatRepository.Setup(x => x.InsertAsync(It.IsAny<EventSeat>())).Callback(() => _timesApplyRuleCalled++);
            _eventSeatRepository.Setup(x => x.UpdateAsync(It.IsAny<EventSeat>())).Callback(() => _timesApplyRuleCalled++);
            _eventSeatRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _eventSeatRepository.Setup(x => x.GetAll()).Returns(_expectedEventSeats.AsQueryable());
            foreach (var eventSeat in _expectedEventSeats)
            {
                _eventSeatRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedEventSeats[eventSeat.Id - 1]);
                _eventSeatRepository.Setup(x => x.GetAllByEventAreaId(eventSeat.EventAreaId)).Returns(_expectedEventSeats.Where(x => x.EventAreaId == eventSeat.EventAreaId).AsQueryable());
            }
        }

        [Test]
        public void Validate_WhenEventSeatFieldEventAreaIdZero_ShouldThrow()
        {
            // arrange
            var eventSeatExpected = new EventSeat
            {
                EventAreaId = default,
                Number = _expectedEventSeats[0].Number,
                Row = _expectedEventSeats[0].Row,
                State = _expectedEventSeats[0].State,
            };
            var strException =
                "The field 'EventAreaId' of EventSeat is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventSeatManagementController.ValidateAsync(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventSeatFieldRowZero_ShouldThrow()
        {
            // arrange
            var eventSeatExpected = new EventSeat
            {
                EventAreaId = _expectedEventSeats[0].EventAreaId,
                Number = _expectedEventSeats[0].Number,
                Row = default,
                State = _expectedEventSeats[0].State,
            };
            var strException =
                "The field 'Row' of EventSeat is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventSeatManagementController.ValidateAsync(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenEventSeatFieldNumberZero_ShouldThrow()
        {
            // arrange
            var eventSeatExpected = new EventSeat
            {
                EventAreaId = _expectedEventSeats[0].EventAreaId,
                Number = default,
                Row = _expectedEventSeats[0].Row,
                State = _expectedEventSeats[0].State,
            };
            var strException =
                "The field 'Number' of EventSeat is not allowed to be null!";

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => _eventSeatManagementController.ValidateAsync(eventSeatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertEventSeat_ShouldNotZeroCallback()
        {
            // arrange
            var eventSeatExpected = new EventSeat(1, 6, 6, 2, State.NotAvailable);

            // act
            await _eventSeatManagementController.InsertEventSeatAsync(eventSeatExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateEventSeat_ShouldNotZeroCallback()
        {
            // arrange
            var eventSeatExpected = new EventSeat(3, 5, 9, 1, State.NotAvailable);

            // act
            await _eventSeatManagementController.UpdateEventSeatAsync(eventSeatExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteEventSeat_ShouldNotZeroCallback()
        {
            // act
            await _eventSeatManagementController.DeleteEventSeatAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnEventSeatById_ShouldNotNull()
        {
            // act
            var actual = await _eventSeatManagementController.GetByIdEventSeatAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnEventSeats_ShouldNotZero()
        {
            // act
            var actual = (await _eventSeatManagementController.GetAllEventSeatsAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}