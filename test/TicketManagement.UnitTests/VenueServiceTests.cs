using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.BusinessLogic.UnitTests
{
    public class VenueServiceTests
    {
        private readonly List<Venue> _expectedVenues = new List<Venue>
        {
            new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568"),
            new Venue(2, "Second venue", "description second venue", "address second venue", "+58487555"),
            new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464"),
        };

        [Test]
        public void Validate_WhenVenueFieldNameEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Name' of Venue is not allowed to be empty!";
            var venueExpected = new Venue(1, "", "description first venue", "address first venue", "+4988955568");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => venueService.Object.Validate(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenVenueFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Description' of Venue is not allowed to be empty!";
            var venueExpected = new Venue(2, "Second venue", "", "address second venue", "+58487555");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => venueService.Object.Validate(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenVenueFieldAddressEmpty_ShouldThrow()
        {
            // arrange
            var strException =
                "The field 'Address' of Venue is not allowed to be empty!";
            var venueExpected = new Venue(3, "Second venue", "description second venue", "", "+84845464");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => venueService.Object.Validate(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [TestCase(1, "First venue", "description first venue", "address first venue", "+4988955568")]
        [TestCase(2, "Second venue", "description second venue", "address second venue", "+58487555")]
        [TestCase(3, "Second venue", "description second venue", "address second venue", "+84845464")]
        public void Validate_WhenNameNonUnique_ShouldTrow(int id, string name, string description, string address, string phone)
        {
            // arrange
            var strException =
                "The Venue name is not unique!";
            var venueExpected = new Venue(id: id, name: name, description: description, address: address, phone: phone);
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            venueRepository.Setup(x => x.GetIdFirstByName(name)).Returns(1);
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };

            // act
            var actualException = Assert.Throws<ValidationException>(
                            () => venueService.Object.Validate(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Insert_WhenInsertVenue_ShouldNotNull()
        {
            // arrange
            var venueExpected = new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };
            venueService.Setup(x => x.Insert(It.IsAny<Venue>())).Returns(1);

            // act
            var actual = venueService.Object.Insert(venueExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Update_WhenUpdateVenue_ShouldNotNull()
        {
            // arrange
            var venueExpected = new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };
            venueService.Setup(x => x.Update(It.IsAny<Venue>())).Returns(1);

            // act
            var actual = venueService.Object.Update(venueExpected);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Delete_WhenDeleteVenue_ShouldNotNull()
        {
            // arrange
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };
            venueService.Setup(x => x.Delete(It.IsAny<int>())).Returns(1);

            // act
            var actual = venueService.Object.Delete(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetById_WhenReturnVenueById_ShouldNotNull()
        {
            // arrange
            var venueExpected = new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464");
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };
            venueService.Setup(x => x.GetById(It.IsAny<int>())).Returns(venueExpected);

            // act
            var actual = venueService.Object.GetById(5444);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetAll_WhenReturnVenues_ShouldNotNull()
        {
            // arrange
            var venueRepository = new Mock<IVenueRepository> { CallBase = true };
            var venueService = new Mock<VenueService>(venueRepository.Object) { CallBase = true };
            venueService.Setup(x => x.GetAll()).Returns(_expectedVenues);

            // act
            var actual = venueService.Object.GetAll();

            // assert
            Assert.NotNull(actual);
        }
    }
}
