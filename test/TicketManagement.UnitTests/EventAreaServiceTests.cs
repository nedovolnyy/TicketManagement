using System.Data.SqlClient;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;

namespace TicketManagement.BusinessLogic.UnitTests
{
    [TestFixture]
    public class EventAreaServiceTests
    {
        private EventAreaService _eventAreaService;

        [SetUp]
        public void Setup()
        {
            _eventAreaService = new EventAreaService();
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventAreaService_GetById_WhenNonExistentId_ShouldThrow(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have eventAreas to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventAreaService.GetById(id));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }
    }
}
