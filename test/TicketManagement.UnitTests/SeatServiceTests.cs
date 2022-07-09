using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class SeatServiceTests
    {
        private static readonly Mock<ISeatRepository> _seatRepository = new Mock<ISeatRepository> { CallBase = true };
        private readonly SeatService _seatService = new SeatService(_seatRepository.Object);
        private readonly List<Seat> _expectedSeats = new List<Seat>
        {
            new Seat(1, 1, 1, 1),
            new Seat(2, 1, 2, 2),
            new Seat(3, 2, 1, 1),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _seatRepository.Setup(x => x.InsertAsync(It.IsAny<Seat>())).Callback(() => _timesApplyRuleCalled++);
            _seatRepository.Setup(x => x.UpdateAsync(It.IsAny<Seat>())).Callback(() => _timesApplyRuleCalled++);
            _seatRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _seatRepository.Setup(x => x.GetAll()).Returns(_expectedSeats.AsQueryable());
            foreach (var seat in _expectedSeats)
            {
                _seatRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedSeats[seat.Id - 1]);
                _seatRepository.Setup(x => x.GetAllByAreaId(seat.AreaId)).Returns(_expectedSeats.Where(x=> x.AreaId == seat.AreaId).AsQueryable());
            }
        }

        [Test]
        public void Validate_WhenSeatFieldAreaIdZero_ShouldThrow()
        {
            // arrange
            var seatExpected = new Seat
            {
                AreaId = default,
                Number = _expectedSeats[0].Number,
                Row = _expectedSeats[0].Row,
            };
            var strException =
                "The field 'AreaId' of Seat is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _seatService.ValidateAsync(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenSeatFieldRowZero_ShouldThrow()
        {
            // arrange
            var seatExpected = new Seat
            {
                AreaId = _expectedSeats[0].AreaId,
                Number = _expectedSeats[0].Number,
                Row = default,
            };
            var strException =
                "The field 'Row' of Seat is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _seatService.ValidateAsync(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenSeatFieldNumberZero_ShouldThrow()
        {
            // arrange
            var seatExpected = new Seat
            {
                AreaId = _expectedSeats[0].AreaId,
                Number = default,
                Row = _expectedSeats[0].Row,
            };
            var strException =
                "The field 'Number' of Seat is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _seatService.ValidateAsync(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenRowAndNumberNonUniqueForArea_ShouldTrow()
        {
            // arrange
            var seatExpected = new Seat
            {
                AreaId = _expectedSeats[1].AreaId,
                Number = _expectedSeats[1].Number,
                Row = _expectedSeats[1].Row,
            };
            var strException =
                "Row and number should be unique for area!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _seatService.ValidateAsync(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertSeat_ShouldNotZeroCallback()
        {
            // arrange
            var seatExpected = new Seat(2, 1, 6);

            // act
            await _seatService.InsertAsync(seatExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateSeat_ShouldNotZeroCallback()
        {
            // arrange
            var seatExpected = new Seat(1, 6, 2, 1);

            // act
            await _seatService.UpdateAsync(seatExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteSeat_ShouldNotZeroCallback()
        {
            // act
            await _seatService.DeleteAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnSeatById_ShouldNotNull()
        {
            // act
            var actual = await _seatService.GetByIdAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnSeats_ShouldNotZero()
        {
            // act
            var actual = (await _seatService.GetAllAsync()).Count();

            // assert
            Assert.NotZero(actual);
        }
    }
}
