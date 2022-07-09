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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await areaService.Object.ValidateAsync(areaExpected));

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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await areaService.Object.ValidateAsync(areaExpected));

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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await areaService.Object.ValidateAsync(areaExpected));

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
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await areaService.Object.ValidateAsync(areaExpected));

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
            areaRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedAreas.AsQueryable());
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await areaService.Object.ValidateAsync(areaExpected));

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
            areaService.Setup(x => x.InsertAsync(It.IsAny<Area>()));

            // act
            var actual = areaService.Object.InsertAsync(areaExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task Update_WhenUpdateArea_ShouldNotNull()
        {
            // arrange
            int timesApplyRuleCalled = default;
            var areaExpected = new Area(1, 2, "First area of second layout", 2, 4);
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.UpdateAsync(It.IsAny<Area>())).Callback(() => timesApplyRuleCalled++);

            // act
            await areaService.Object.UpdateAsync(areaExpected);

            // assert
            Assert.NotZero(timesApplyRuleCalled);
        }

        [Test]
        public void Delete_WhenDeleteArea_ShouldNotNull()
        {
            // arrange
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.DeleteAsync(It.IsAny<int>()));

            // act
            var actual = areaService.Object.DeleteAsync(1);

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
            areaService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(areaExpected);

            // act
            var actual = areaService.Object.GetByIdAsync(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnAreas_ShouldNotNull()
        {
            // arrange
            var areaRepository = new Mock<IAreaRepository> { CallBase = true };
            var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };
            areaService.Setup(x => x.GetAllAsync()).ReturnsAsync(_expectedAreas);

            // act
            var actual = areaService.Object.GetAllAsync();

            // assert
            Assert.NotNull(actual);
        }
    }
}
