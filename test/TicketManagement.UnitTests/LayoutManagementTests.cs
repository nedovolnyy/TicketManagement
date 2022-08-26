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
    public class LayoutManagementTests
    {
        private static readonly Mock<ILayoutRepository> _layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
        private readonly LayoutManagementController _layoutManagementController = new LayoutManagementController(_layoutRepository.Object);
        private readonly List<Layout> _expectedLayouts = new List<Layout>
        {
            new Layout(1, "First layout", 1, "description first layout"),
            new Layout(2, "Second layout", 1, "description second layout"),
            new Layout(3, "Second layout", 2, "description second layout"),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _layoutRepository.Setup(x => x.InsertAsync(It.IsAny<Layout>())).Callback(() => _timesApplyRuleCalled++);
            _layoutRepository.Setup(x => x.UpdateAsync(It.IsAny<Layout>())).Callback(() => _timesApplyRuleCalled++);
            _layoutRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _layoutRepository.Setup(x => x.GetAll()).Returns(_expectedLayouts.AsQueryable());
            foreach (var layout in _expectedLayouts)
            {
                _layoutRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedLayouts[layout.Id - 1]);
                _layoutRepository.Setup(x => x.GetAllByVenueId(layout.VenueId)).Returns(_expectedLayouts.Where(x => x.VenueId == layout.VenueId).AsQueryable());
            }
        }

        [Test]
        public void Validate_WhenLayoutFieldNameEmpty_ShouldThrow()
        {
            // arrange
            var layoutExpected = new Layout
            {
                Name = string.Empty,
                Description = _expectedLayouts[0].Description,
                VenueId = _expectedLayouts[0].VenueId,
            };
            var strException =
                "The field 'Name' of Layout is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _layoutManagementController.ValidateAsync(layoutExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenLayoutFieldVenueIdZero_ShouldThrow()
        {
            // arrange
            var layoutExpected = new Layout
            {
                Name = _expectedLayouts[0].Name,
                Description = _expectedLayouts[0].Description,
                VenueId = default,
            };
            var strException =
                "The field 'VenueId' of Layout is not allowed to be null!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _layoutManagementController.ValidateAsync(layoutExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenLayoutFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var layoutExpected = new Layout
            {
                Name = _expectedLayouts[0].Name,
                Description = string.Empty,
                VenueId = _expectedLayouts[0].VenueId,
            };
            var strException =
                "The field 'Description' of Layout is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _layoutManagementController.ValidateAsync(layoutExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow()
        {
            // arrange
            var layoutExpected = new Layout
            {
                Name = _expectedLayouts[0].Name,
                Description = "any",
                VenueId = _expectedLayouts[0].VenueId,
            };
            var strException =
                "Layout name should be unique in venue!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _layoutManagementController.ValidateAsync(layoutExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertLayout_ShouldNotZeroCallback()
        {
            // arrange
            var layoutExpected = new Layout("1st layout", 1, "any description for first layout");

            // act
            await _layoutManagementController.InsertLayoutAsync(layoutExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateLayout_ShouldNotZeroCallback()
        {
            // arrange
            var layoutExpected = new Layout(3, "2nd layout", 2, "any description for second layout");

            // act
            await _layoutManagementController.UpdateLayoutAsync(layoutExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteLayout_ShouldNotZeroCallback()
        {
            // act
            await _layoutManagementController.DeleteLayoutAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnLayoutById_ShouldNotNull()
        {
            // act
            var actual = await _layoutManagementController.GetByIdLayoutAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnLayouts_ShouldNotZero()
        {
            // act
            var actual = (await _layoutManagementController.GetAllLayoutsAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}
