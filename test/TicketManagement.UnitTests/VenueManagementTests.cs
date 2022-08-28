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
    public class VenueManagementTests
    {
        private static readonly Mock<IVenueRepository> _venueRepository = new Mock<IVenueRepository> { CallBase = true };
        private readonly VenueManagementController _venueManagementController = new VenueManagementController(_venueRepository.Object);
        private readonly List<Venue> _expectedVenues = new List<Venue>
        {
            new Venue(1, "First venue", "description first venue", "address first venue", "+4988955568"),
            new Venue(2, "Second venue", "description second venue", "address second venue", "+58487555"),
            new Venue(3, "Second venue", "description second venue", "address second venue", "+84845464"),
        };
        private int _timesApplyRuleCalled;

        [SetUp]
        protected void SetUp()
        {
            _venueRepository.Setup(x => x.InsertAsync(It.IsAny<Venue>())).Callback(() => _timesApplyRuleCalled++);
            _venueRepository.Setup(x => x.UpdateAsync(It.IsAny<Venue>())).Callback(() => _timesApplyRuleCalled++);
            _venueRepository.Setup(x => x.DeleteAsync(It.IsAny<int>())).Callback(() => _timesApplyRuleCalled++);
            _venueRepository.Setup(x => x.GetAll()).Returns(_expectedVenues.AsQueryable());
            foreach (var venue in _expectedVenues)
            {
                _venueRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_expectedVenues[venue.Id - 1]);
                _venueRepository.Setup(x => x.GetIdFirstByNameAsync(venue.Name)).ReturnsAsync(venue.Id);
            }
        }

        [Test]
        public void Validate_WhenVenueFieldNameEmpty_ShouldThrow()
        {
            // arrange
            var venueExpected = new Venue
            {
                Name = string.Empty,
                Address = _expectedVenues[0].Address,
                Description = _expectedVenues[0].Description,
                Phone = _expectedVenues[0].Phone,
            };
            var strException =
                "The field 'Name' of Venue is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _venueManagementController.ValidateAsync(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenVenueFieldDescriptionEmpty_ShouldThrow()
        {
            // arrange
            var venueExpected = new Venue
            {
                Name = _expectedVenues[0].Name,
                Address = _expectedVenues[0].Address,
                Description = string.Empty,
                Phone = _expectedVenues[0].Phone,
            };
            var strException =
                "The field 'Description' of Venue is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _venueManagementController.ValidateAsync(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenVenueFieldAddressEmpty_ShouldThrow()
        {
            // arrange
            var venueExpected = new Venue
            {
                Name = _expectedVenues[0].Name,
                Address = string.Empty,
                Description = _expectedVenues[0].Description,
                Phone = _expectedVenues[0].Phone,
            };
            var strException =
                "The field 'Address' of Venue is not allowed to be empty!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _venueManagementController.ValidateAsync(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public void Validate_WhenNameNonUnique_ShouldTrow()
        {
            // arrange
            var venueExpected = new Venue
            {
                Name = _expectedVenues[0].Name,
                Address = "any",
                Description = "any",
            };
            var strException =
                "The Venue name is not unique!";

            // act
            var actualException = Assert.ThrowsAsync<ValidationException>(
                            async () => await _venueManagementController.ValidateAsync(venueExpected));

            // assert
            Assert.That(actualException.Message, Is.EqualTo(strException));
        }

        [Test]
        public async Task Insert_WhenCallInsertVenue_ShouldNotZeroCallback()
        {
            // arrange
            var venueExpected = new Venue(3, "2nd venue", "any description for second venue", "address second venue", "+84845464");

            // act
            await _venueManagementController.InsertVenueAsync(venueExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Update_WhenCallUpdateVenue_ShouldNotZeroCallback()
        {
            // arrange
            var venueExpected = new Venue(1, "1st venue", "any description for first venue", "address first venue", "+4988955568");

            // act
            await _venueManagementController.UpdateVenueAsync(venueExpected);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task Delete_WhenCallDeleteVenue_ShouldNotZeroCallback()
        {
            // act
            await _venueManagementController.DeleteVenueAsync(1);

            // assert
            Assert.NotZero(_timesApplyRuleCalled);
            _timesApplyRuleCalled = default;
        }

        [Test]
        public async Task GetById_WhenReturnVenueById_ShouldNotNull()
        {
            // act
            var actual = await _venueManagementController.GetByIdVenueAsync(1);

            // assert
            Assert.NotNull(actual);
        }

        [Test]
        public async Task GetAll_WhenReturnVenues_ShouldNotZero()
        {
            // act
            var actual = (await _venueManagementController.GetAllVenuesAsync()).Count;

            // assert
            Assert.NotZero(actual);
        }
    }
}
