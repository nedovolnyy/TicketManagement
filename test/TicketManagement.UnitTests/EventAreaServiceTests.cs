using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventAreaServiceTests
    {
        private readonly List<EventArea> _expectedEventAreas = new List<EventArea>
        {
            new EventArea(1, 2, "First eventArea of second layout", 2, 4, 5.8m),
            new EventArea(2, 1, "First eventArea of first layout", 3, 2, 8.6m),
            new EventArea(3, 2, "First eventArea of second layout", 1, 7, 4.6m),
        };
        private EventAreaService _eventAreaService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _eventAreaService = new EventAreaService();
        }

        [TestCase(1, 2, "First eventArea of second layout", 2, 4, 7.5)]
        [TestCase(2, 1, "First eventArea of first layout", 3, 2, 5.5)]
        [TestCase(3, 2, "First eventArea of second layout", 1, 7, 4.3)]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var eventAreaExpected = new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price);
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Insert(It.IsAny<EventArea>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = eventAreaService.Object;
            mockedInstance.Insert(eventAreaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(1, 2, "First eventArea of second layout", 2, 4, 7.5)]
        [TestCase(2, 1, "First eventArea of first layout", 3, 2, 5.5)]
        [TestCase(3, 2, "First eventArea of second layout", 1, 7, 4.3)]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, int eventId, string description, int coordX, int coordY, decimal price)
        {
            // arrange
            var eventAreaExpected = new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price);
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Update(It.IsAny<EventArea>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = eventAreaService.Object;
            mockedInstance.Update(eventAreaExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenCallbackDelete_ShouldTrue(int id)
        {
            // arrange
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = eventAreaService.Object;
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
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = eventAreaService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnEventAreaById_ShouldNotNull(int id)
        {
            // arrange
            var eventAreaExpected = new EventArea(id, 2, "First eventArea of first layout", 3, 2, 8.1m);
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventAreaExpected);
            var mockedInstance = eventAreaService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnEventAreas_ShouldNotNull()
        {
            // arrange
            var eventAreaService = new Mock<IService<EventArea>> { CallBase = true };

            // act
            eventAreaService.Setup(x => x.GetAll()).Returns(_expectedEventAreas);
            var mockedInstance = eventAreaService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
