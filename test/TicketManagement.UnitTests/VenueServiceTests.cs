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
    public class VenueServiceTests
    {
        private readonly List<Venue> _expectedVenues = new List<Venue>
        {
            new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568"),
            new Venue(2, "Second venue", "description second venue", "address second venue", "+58487555"),
            new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464"),
        };
        private VenueService _venueService;
        private int _timesApplyRuleCalled;

        [SetUp]
        public void Setup()
        {
            _venueService = new VenueService();
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Validate_WhenNameNonUnique_ShouldTrow(int id, string name, string description, string address, string phone)
        {
            // arrange
            string strException =
                "The Venue name has not unique!";
            var venueExpected = new Venue(id: id, name: name, description: description, address: address, phone: phone);
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            venueRepository.Setup(x => x.GetFirstByName(name)).Returns(venueExpected);
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };

            // act
            var ex = Assert.Throws<ValidationException>(
                            () => venueService.Object.Validate(venueExpected));

            // assert
            Assert.That(ex.Message, Is.EqualTo(strException));
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Insert_WhenCallbackInsert_ShouldTrue(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var venueExpected = new Venue(id: id, name: name, description: description, address: address, phone: phone);
                    var venueService = new Mock<IService<Venue>> { CallBase = true };

                    // act
                    venueService.Setup(x => x.Insert(It.IsAny<Venue>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = venueService.Object;
                    mockedInstance.Insert(venueExpected);

                    // assert
                    Assert.NotZero(_timesApplyRuleCalled);
                    _timesApplyRuleCalled = 0;
                }
            }
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Update_WhenCallbackUpdate_ShouldTrue(int id, string name, string description, string address, string phone)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (var mock = AutoMock.GetLoose())
                {
                    // arrange
                    var venueExpected = new Venue(id: id, name: name, description: description, address: address, phone: phone);
                    var venueService = new Mock<IService<Venue>> { CallBase = true };

                    // act
                    venueService.Setup(x => x.Update(It.IsAny<Venue>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = venueService.Object;
                    mockedInstance.Update(venueExpected);

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
                    var venueService = new Mock<IService<Venue>> { CallBase = true };

                    // act
                    venueService.Setup(x => x.Delete(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
                    var mockedInstance = venueService.Object;
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
            var venueService = new Mock<IService<Venue>> { CallBase = true };

            // act
            venueService.Setup(x => x.GetById(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            var mockedInstance = venueService.Object;
            mockedInstance.GetById(id);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = 0;
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void GetById_WhenReturnVenueById_ShouldNotNull(int id)
        {
            // arrange
            var venueExpected = new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464");
            var venueService = new Mock<IService<Venue>> { CallBase = true };

            // act
            venueService.Setup(x => x.GetById(It.IsAny<int>())).Returns(venueExpected);
            var mockedInstance = venueService.Object;
            var e = mockedInstance.GetById(id);

            // assert
            Assert.NotNull(e);
        }

        [Test]
        public void GetAll_WhenReturnVenues_ShouldNotNull()
        {
            // arrange
            var venueService = new Mock<IService<Venue>> { CallBase = true };

            // act
            venueService.Setup(x => x.GetAll()).Returns(_expectedVenues);
            var mockedInstance = venueService.Object;
            var e = mockedInstance.GetAll();

            // assert
            Assert.NotNull(e);
        }
    }
}
