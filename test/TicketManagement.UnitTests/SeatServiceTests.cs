using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class SeatServiceTests
    {
        private readonly List<Seat> _expectedSeats = new List<Seat>
        {
            new Seat(1, 1, 1, 1),
            new Seat(2, 1, 2, 2),
            new Seat(3, 2, 1, 1),
        };

        [Test]
        public void Validate_WhenSeatFieldAreaIdNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'AreaId' of Seat is not allowed to be null!";
            var seatExpected = new Seat(1, 0, 1, 1);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => seatService.Object.Validate(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenSeatFieldRowNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Row' of Seat is not allowed to be null!";
            var seatExpected = new Seat(2, 1, 0, 2);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => seatService.Object.Validate(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenSeatFieldNumberNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Number' of Seat is not allowed to be null!";
            var seatExpected = new Seat(3, 2, 1, 0);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => seatService.Object.Validate(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(3, 2, 1, 1)]
        public void Validate_WhenRowAndNumberNonUniqueForSeat_ShouldTrow(int id, int areaId, int row, int number)
        {
            // arrange
            var strException =
                "Row and number should be unique for area!";
            var seatExpected = new Seat(id: id, areaId: areaId, row: row, number: number);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            seatRepository.Setup(x => x.GetAllByAreaId(areaId)).Returns(_expectedSeats);
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => seatService.Object.Validate(seatExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertSeat_ShouldNotNull()
        {
            // arrange
            var seatExpected = new Seat(3, 2, 1, 6);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };
            seatService.Setup(x => x.Insert(It.IsAny<Seat>())).Returns(1);

            // act
            var actual = seatService.Object.Insert(seatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateSeat_ShouldNotNull()
        {
            // arrange
            var seatExpected = new Seat(1, 6, 1, 1);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };
            seatService.Setup(x => x.Update(It.IsAny<Seat>())).Returns(1);

            // act
            var actual = seatService.Object.Update(seatExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteSeat_ShouldNotNull()
        {
            // arrange
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };
            seatService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);

            // act
            var actual = seatService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnSeatById_ShouldNotNull()
        {
            // arrange
            var seatExpected = new Seat(3, 2, 1, 1);
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };
            seatService.Setup(x => x.GetById(It.IsAny<int>())).Returns(seatExpected);

            // act
            var actual = seatService.Object.GetById(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnSeats_ShouldNotNull()
        {
            // arrange
            var seatRepository = new Mock<ISeatRepository> { CallBase = true };
            var seatService = new Mock<SeatService>(seatRepository.Object) { CallBase = true };
            seatService.Setup(x => x.GetAll()).Returns(_expectedSeats);

            // act
            var actual = seatService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
