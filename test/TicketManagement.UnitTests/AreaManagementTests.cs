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
    public class AreaManagementTests
    {
        private static readonly Mock<IAreaRepository> _areaRepository = new Mock<IAreaRepository> { CallBase = true };
        private readonly AreaManagementController _areaManagementController = new AreaManagementController(_areaRepository.Object);
        private readonly List<Area> _expectedAreas = new List<Area>
        {
            new Area(1, 2, "First area of second layout", 2, 4),
            new Area(2, 1, "First area of first layout", 3, 2),
            new Area(3, 2, "First area of second layout", 1, 7),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _areaRepository.Setup(x => x.InsertAsync(It.IsAny<Area>())).Callback(() => _timesApplyRuleCalled++);
            _areaRepository.Setup(x => x.UpdateAsync(It.IsAny<Area>())).Callback(() => _timesApplyRuleCalled++);
            _areaRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _areaRepository.Setup(x => x.GetAll()).Returns(_expectedAreas.AsQueryable());
            foreach (var area in _expectedAreas)
            {
                _areaRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedAreas[area.Id - 1]);
                _areaRepository.Setup(x => x.GetAllByLayoutId(area.LayoutId)).Returns(_expectedAreas.Where(x => x.LayoutId == area.LayoutId).AsQueryable());
            }
        }

        [Test]
        public void Validate_WhenAreaFieldLayoutIdZero_ShouldThrow()
        {
            // arrange
            var areaExpected = new Area
            {
                LayoutId = default,
                Description = _expectedAreas[0].Description,
                CoordX = _expectedAreas[0].CoordX,
                CoordY = _expectedAreas[0].CoordY,
            };
            var strException =
                "The field 'LayoutId' of Area is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _areaManagementController.ValidateAsync(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var areaExpected = new Area
            {
                LayoutId = _expectedAreas[0].LayoutId,
                Description = string.Empty,
                CoordX = _expectedAreas[0].CoordX,
                CoordY = _expectedAreas[0].CoordY,
            };
            var strException =
                "The field 'Description' of Area is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _areaManagementController.ValidateAsync(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldCoordXZero_ShouldThrow()
        {
            // arrange
            var areaExpected = new Area
            {
                LayoutId = _expectedAreas[0].LayoutId,
                Description = _expectedAreas[0].Description,
                CoordX = default,
                CoordY = _expectedAreas[0].CoordY,
            };
            var strException =
                "The field 'CoordX' of Area is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _areaManagementController.ValidateAsync(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenAreaFieldCoordYZero_ShouldThrow()
        {
            // arrange
            var areaExpected = new Area
            {
                LayoutId = _expectedAreas[0].LayoutId,
                Description = _expectedAreas[0].Description,
                CoordX = _expectedAreas[0].CoordX,
                CoordY = default,
            };
            var strException =
                "The field 'CoordY' of Area is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _areaManagementController.ValidateAsync(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenDescriptionNonUnique_ShouldThrow()
        {
            // arrange
            var areaExpected = new Area
            {
                LayoutId = _expectedAreas[0].LayoutId,
                Description = _expectedAreas[0].Description,
                CoordX = int.MaxValue,
                CoordY = int.MaxValue,
            };
            var strException =
                "Area description should be unique for area!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _areaManagementController.ValidateAsync(areaExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertArea_ShouldNotZeroCallback()
        {
            // arrange
            var areaExpected = new Area(2, "1st area of second layout", 2, 4);

            // act
            await _areaManagementController.InsertAreaAsync(areaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateArea_ShouldNotZeroCallback()
        {
            // arrange
            var areaExpected = new Area(1, 2, "1st area of second layout", 2, 4);

            // act
            await _areaManagementController.UpdateAreaAsync(areaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteArea_ShouldNotZeroCallback()
        {
            // act
            await _areaManagementController.DeleteAreaAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnAreaById_ShouldNotNull()
        {
            // act
            var actual = await _areaManagementController.GetByIdAreaAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnAreas_ShouldNotZero()
        {
            // act
            var actual = (await _areaManagementController.GetAllAreasAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}
