using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class AreaServiceTests
    {
        private readonly List<Area> _expectedAreas = new List<Area>
        {
            new Area(1, 2, "First area of second layout", 2, 4),
            new Area(2, 1, "First area of first layout", 3, 2),
            new Area(3, 2, "First area of second layout", 1, 7),
        };

        [Test]
        public void Validate_WhenAreaFieldLayoutIdNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'LayoutId' of Area is not allowed to be null!";
            var areaExpected = new Area(1, 0, "First area of second layout", 2, 4);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => areaService.Object.Validate(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Description' of Area is not allowed to be empty!";
            var areaExpected = new Area(2, 1, "", 3, 2);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => areaService.Object.Validate(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldCoordXNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'CoordX' of Area is not allowed to be null!";
            var areaExpected = new Area(3, 2, "First area of second layout", 0, 3);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => areaService.Object.Validate(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldCoordYNull_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'CoordY' of Area is not allowed to be null!";
            var areaExpected = new Area(3, 2, "First area of second layout", 1, 0);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => areaService.Object.Validate(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Validate_WhenDescriptionNonUnique_ShouldThrow(int id, int layoutId, string description, int coordX, int coordY)
        {
            // arrange
            var strException =
                "Area description should be unique for area!";
            var areaExpected = new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            areaRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedAreas);
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => areaService.Object.Validate(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertArea_ShouldNotNull()
        {
            // arrange
            var areaExpected = new Area(1, 2, "First area of second layout", 2, 4);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.Insert(It.IsAny<Area>())).Returns(1);

            // act
            var actual = areaService.Object.Insert(areaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateArea_ShouldNotNull()
        {
            // arrange
            var areaExpected = new Area(1, 2, "First area of second layout", 2, 4);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.Update(It.IsAny<Area>())).Returns(1);

            // act
            var actual = areaService.Object.Update(areaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteArea_ShouldNotNull()
        {
            // arrange
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);

            // act
            var actual = areaService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnAreaById_ShouldNotNull()
        {
            // arrange
            var areaExpected = new Area(5444, 2, "First area of first layout", 3, 2);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.GetById(It.IsAny<int>())).Returns(areaExpected);

            // act
            var actual = areaService.Object.GetById(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnAreas_ShouldNotNull()
        {
            // arrange
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.GetAll()).Returns(_expectedAreas);

            // act
            var actual = areaService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
