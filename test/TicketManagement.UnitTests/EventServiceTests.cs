using System;
using System.Collections.Generic;
using System.Transactions;
using Autofac.Extras.Moq;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventServiceTests
    {
        private readonly List<Event> _expectedEvents = new List<Event>
        {
            new Event(1, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2),
            new Event(2, "Stanger Things Serie", DateTimeOffset.Parse("09/19/2022"), "Stanger Things Serie", 1),
        };
        private EventService _evntService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _evntService = new EventService();
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2021", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2021", "Stanger Things Serie")]
        public void Validate_WhenEventTimeInPast_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Event can't be created in the past!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(2, 1, "Stanger Things Serie", "2022-09-09 00:00:00.0000000 +03:00", "Things Serie")]
        public void Validate_WhenEventInSameTimeForLayout_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Do not create event for the same layout in the same time!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Validate_WhenLayoutNameNonUniqueInVenue_ShouldTrow(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            // arrange
            string strException =
                "Layout name should be unique in venue!";
            var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
            var evntRepository = new Mock<IEventRepository> { CallBase = true };
            evntRepository.Setup(x => x.GetAllByLayoutId(layoutId)).Returns(_expectedEvents);
            var evntService = new Mock<EventService>(evntRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => evntService.Object.Validate(evntExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
                    var evntService = new Mock<IService<Event>> { CallBase = true };

                    // act
                    evntService.Setup(x => x.Insert(It.IsAny<Event>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = evntService.Object;
                    mockedInstance.Insert(evntExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(1, 2, "Kitchen Serie", "09/09/2022", "Kitchen Serie")]
        [TestCase(2, 1, "Stanger Things Serie", "09/19/2022", "Stanger Things Serie")]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, int layoutId, string name, DateTimeOffset eventTime, string description)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var evntExpected = new Event(id: id, layoutId: layoutId, name: name, eventTime: eventTime, description: description);
                    var evntService = new Mock<IService<Event>> { CallBase = true };

                    // act
                    evntService.Setup(x => x.Update(It.IsAny<Event>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = evntService.Object;
                    mockedInstance.Update(evntExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenCallbackDelete_ShouldTrue(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var evntService = new Mock<IService<Event>> { CallBase = true };

                    // act
                    evntService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = evntService.Object;
                    mockedInstance.Delete(id);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenCallbackGetById_ShouldTrue(int id)
        {
            // arrange
            var evntService = new Mock<IService<Event>> { CallBase = true };

            // act
            evntService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = evntService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnEventById_ShouldNotNull(int id)
        {
            // arrange
            var evntExpected = new Event(id, "Kitchen Serie", DateTimeOffset.Parse("09/09/2022"), "Kitchen Serie", 2);
            var evntService = new Mock<IService<Event>> { CallBase = true };

            // act
            evntService.Setup(x => x.GetById(It.IsAny<int>())).Returns(evntExpected);
            var mockedInstance = evntService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnEvents_ShouldNotNull()
        {
            // arrange
            var evntService = new Mock<IService<Event>> { CallBase = true };

            // act
            evntService.Setup(x => x.GetAll()).Returns(_expectedEvents);
            var mockedInstance = evntService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
