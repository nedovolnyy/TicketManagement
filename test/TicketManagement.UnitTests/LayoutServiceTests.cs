using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class LayoutServiceTests
    {
        private readonly List<Layout> _expectedLayouts = new List<Layout>
        {
            new Layout(1, "First layout", 1, "description first layout"),
            new Layout(2, "Second layout", 1, "description second layout"),
            new Layout(3, "Second layout", 2, "description second layout"),
        };

        [TestCase(1, "", 1, "description first layout")]
        [TestCase(2, "Second layout", 0, "description second layout")]
        [TestCase(3, "Second layout", 2, "")]
        public void Validate_WhenLayoutFieldNull_ShouldThrow(int id, string name, int venueId, string description)
        {
            // arrange
            var strException =
                "The field of Layout is not allowed to be null!";
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
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
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow(int id, string name, int venueId, string description)
        {
            // arrange
            var strException =
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
        public void Insert_WhenInsertLayout_ShouldNotNull(int id, string name, int venueId, string description)
        {
            // arrange
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            layoutService.Setup(x => x.Insert(It.IsAny<Layout>())).Returns(1);
            var actual = layoutService.Object.Insert(layoutExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(3, "Second layout", 2, "description second layout")]
        public void Update_WhenUpdateLayout_ShouldNotNull(int id, string name, int venueId, string description)
        {
            // arrange
            var layoutExpected = new Layout(id: id, name: name, venueId: venueId, description: description);
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            layoutService.Setup(x => x.Update(It.IsAny<Layout>())).Returns(1);
            var actual = layoutService.Object.Update(layoutExpected);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(1)]
        public void Delete_WhenDeleteLayout_ShouldNotNull(int id)
        {
            // arrange
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            layoutService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);
            var actual = layoutService.Object.Delete(id);

            // assert
            Assert.NotNull(actual);
        }

        [TestCase(5444)]
        public void GetById_WhenReturnLayoutById_ShouldNotNull(int id)
        {
            // arrange
            var layoutExpected = new Layout(1, "First layout", 1, "description first layout");
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            layoutService.Setup(x => x.GetById(It.IsAny<int>())).Returns(layoutExpected);
            var actual = layoutService.Object.GetById(id);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnLayouts_ShouldNotNull()
        {
            // arrange
            var layoutRepository = new Mock<ILayoutRepository> { CallBase = true };
            var layoutService = new Mock<LayoutService>(layoutRepository.Object) { CallBase = true };

            // act
            layoutService.Setup(x => x.GetAll()).Returns(_expectedLayouts);
            var actual = layoutService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
