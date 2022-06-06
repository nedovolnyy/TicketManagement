﻿using System.Collections.Generic;
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
    public class AreaServiceTests
    {
        private readonly List<Area> _expectedAreas = new List<Area>
        {
            new Area(1, 2, "First area of second layout", 2, 4),
            new Area(2, 1, "First area of first layout", 3, 2),
            new Area(3, 2, "First area of second layout", 1, 7),
        };
        private AreaService _areaService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _areaService = new AreaService();
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Validate_WhenCallbackValidate_ShouldTrue(int id, int layoutId, string description, int coordX, int coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var areaExpected = new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY);
                    var db = new Mock<DatabaseContext> { CallBase = true };
                    var areaRepository = new Mock<AreaRepository>(db.Object) { CallBase = true };
                    var areaService = new Mock<AreaService>(areaRepository.Object) { CallBase = true };

                    // act
                    areaService.Protected().Setup("Validate", areaExpected).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = areaService.Object;
                    mockedInstance.Insert(areaExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, int layoutId, string description, int coordX, int coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var areaExpected = new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY);
                    var areaService = new Mock<IService<Area>> { CallBase = true };

                    // act
                    areaService.Setup(x => x.Insert(It.IsAny<Area>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = areaService.Object;
                    mockedInstance.Insert(areaExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(1, 2, "First area of second layout", 2, 4)]
        [TestCase(2, 1, "First area of first layout", 3, 2)]
        [TestCase(3, 2, "First area of second layout", 1, 7)]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, int layoutId, string description, int coordX, int coordY)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var areaExpected = new Area(id: id, layoutId: layoutId, description: description, coordX: coordX, coordY: coordY);
                    var areaService = new Mock<IService<Area>> { CallBase = true };

                    // act
                    areaService.Setup(x => x.Update(It.IsAny<Area>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = areaService.Object;
                    mockedInstance.Update(areaExpected);

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
                    var areaService = new Mock<IService<Area>> { CallBase = true };

                    // act
                    areaService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = areaService.Object;
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
            var areaService = new Mock<IService<Area>> { CallBase = true };

            // act
            areaService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = areaService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnAreaById_ShouldNotNull(int id)
        {
            // arrange
            var areaExpected = new Area(id, 2, "First area of first layout", 3, 2);
            var areaService = new Mock<IService<Area>> { CallBase = true };

            // act
            areaService.Setup(x => x.GetById(It.IsAny<int>())).Returns(areaExpected);
            var mockedInstance = areaService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnAreas_ShouldNotNull()
        {
            // arrange
            var areaService = new Mock<IService<Area>> { CallBase = true };

            // act
            areaService.Setup(x => x.GetAll()).Returns(_expectedAreas);
            var mockedInstance = areaService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
