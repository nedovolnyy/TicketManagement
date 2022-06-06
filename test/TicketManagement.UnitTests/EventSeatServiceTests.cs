using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Autofac.Extras.Moq;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Interfaces;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventSeatServiceTests
    {
        private readonly List<EventSeat> _expectedEventSeats = new List<EventSeat>
        {
            new EventSeat(1, 6, 56, 2, 4),
            new EventSeat(2, 7, 3, 3, 2),
            new EventSeat(3, 5, 9, 1, 7),
        };
        private EventSeatService _eventSeatService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _eventSeatService = new EventSeatService();
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Insert_WhenInsertSameId_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    string strException =
                        "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                        "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                        "The statement has been terminated.";
                    var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
                    var db = new Mock<DatabaseContext> { CallBase = true };
                    var eventSeatRepository = new Mock<EventSeatRepository>(db.Object) { CallBase = true };
                    var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

                    // act
                    var mockedInstance = eventSeatService.Object;
                    var ex = Assert.Throws<SqlException>(() => mockedInstance.Insert(eventSeatExpected));

                    // assert
                    Assert.That(ex.Message, Is.EqualTo(strException));
                }
            }
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Update_WhenUpdateSameId_ShouldThrow(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    string strException =
                        "The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_Area_EventSeat\". " +
                        "The conflict occurred in database \"TicketManagement.Database\", table \"dbo.EventArea\", column 'Id'.\r\n" +
                        "The statement has been terminated.";
                    var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
                    var db = new Mock<DatabaseContext> { CallBase = true };
                    var eventSeatRepository = new Mock<EventSeatRepository>(db.Object) { CallBase = true };
                    var eventSeatService = new Mock<EventSeatService>(eventSeatRepository.Object) { CallBase = true };

                    // act
                    var mockedInstance = eventSeatService.Object;
                    var ex = Assert.Throws<SqlException>(() => mockedInstance.Update(eventSeatExpected));

                    // assert
                    Assert.That(ex.Message, Is.EqualTo(strException));
                }
            }
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
                    var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

                    // act
                    eventSeatService.Setup(x => x.Insert(It.IsAny<EventSeat>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = eventSeatService.Object;
                    mockedInstance.Insert(eventSeatExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(1, 6, 56, 2, 4)]
        [TestCase(2, 7, 3, 3, 2)]
        [TestCase(3, 5, 9, 1, 7)]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, int eventAreaId, int row, int number, int state)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var eventSeatExpected = new EventSeat(id: id, eventAreaId: eventAreaId, row: row, number: number, state: state);
                    var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

                    // act
                    eventSeatService.Setup(x => x.Update(It.IsAny<EventSeat>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = eventSeatService.Object;
                    mockedInstance.Update(eventSeatExpected);

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
                    var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

                    // act
                    eventSeatService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = eventSeatService.Object;
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
            var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

            // act
            eventSeatService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = eventSeatService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnEventSeatById_ShouldNotNull(int id)
        {
            // arrange
            var eventSeatExpected = new EventSeat(id, 6, 56, 2, 4);
            var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

            // act
            eventSeatService.Setup(x => x.GetById(It.IsAny<int>())).Returns(eventSeatExpected);
            var mockedInstance = eventSeatService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnEventSeats_ShouldNotNull()
        {
            // arrange
            var eventSeatService = new Mock<IService<EventSeat>> { CallBase = true };

            // act
            eventSeatService.Setup(x => x.GetAll()).Returns(_expectedEventSeats);
            var mockedInstance = eventSeatService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
