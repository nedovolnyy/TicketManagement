using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class LayoutServiceTests
    {
        private readonly List<Layout> _expectedLayouts = new List<Layout>
        {
            new Layout(1, "First layout", 1, "description first layout"),
            new Layout(2, "Second layout", 1, "description second layout"),
            new Layout(3, "Second layout", 2, "description second layout"),
        };
        private LayoutService _layoutService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _layoutService = new LayoutService();
        }

        [TestCase(1, "First layout", 1, "description first layout")]
        [TestCase(2, "Second layout", 1, "description second layout")]
        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow(int id, string name, int venueId, string description)
        {
            // arrange
            string strException =
                "Layout name should be unique in venue!";
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            layoutRepository.Setup(x => x.GetAllByVenueId(venueId)).Returns(_expectedLayouts);
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => layoutService.Object.Validate(layoutExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, "First layout", 1, "description first layout")]
        [TestCase(2, "Second layout", 1, "description second layout")]
        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, string name, int venueId, string description)
        {
            // arrange
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.Insert(It.IsAny<Layout>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = layoutService.Object;
            mockedInstance.Insert(layoutExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(1, "First layout", 1, "description first layout")]
        [TestCase(2, "Second layout", 1, "description second layout")]
        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, string name, int venueId, string description)
        {
            // arrange
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.Update(It.IsAny<Layout>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = layoutService.Object;
            mockedInstance.Update(layoutExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenCallbackDelete_ShouldTrue(int id)
        {
            // arrange
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = layoutService.Object;
            mockedInstance.Delete(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenCallbackGetById_ShouldTrue(int id)
        {
            // arrange
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = layoutService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnLayoutById_ShouldNotNull(int id)
        {
            // arrange
            var layoutExpected = new Layout(1, "First layout", 1, "description first layout");
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.GetById(It.IsAny<int>())).Returns(layoutExpected);
            var mockedInstance = layoutService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnLayouts_ShouldNotNull()
        {
            // arrange
            var layoutService = new Mock<IService<Layout>> { CallBase = true };

            // act
            layoutService.Setup(x => x.GetAll()).Returns(_expectedLayouts);
            var mockedInstance = layoutService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
