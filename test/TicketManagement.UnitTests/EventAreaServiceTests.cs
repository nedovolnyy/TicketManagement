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

        [TestCase(2, 2, "First eventArea of second event", 4, 6, 345366)]
        [TestCase(1, 1, "First eventArea of first event", 1, 1, 35366)]
        public void EventAreaService_Delete_ByEventArea_Exc_refTable(int? id, int? eventId, string description, int? coordX, int? coordY, decimal? price)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have eventAreas to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventAreaService.Delete(new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(23525, 8, "Figngngndgndgnout", 4, 4, 4563)]
        public void EventAreaService_Delete_ByEventArea_Exc_HaventEventArea(int? id, int? eventId, string description, int? coordX, int? coordY, decimal? price)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Don't have eventAreas to show!";

                // act
                var ex = Assert.Throws<ValidationException>(
                                () => _eventAreaService.Delete(new EventArea(id: id, eventId: eventId, description: description, coordX: coordX, coordY: coordY, price: price)));

                // assert
                Assert.That(ex.Message, Is.EqualTo(strException));
            }
        }

        [TestCase(-65464)]
        [TestCase(000033366)]
        [TestCase(5444)]
        public void EventAreaService_GetById_Exc_noEventArea(int id)
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

        [Test]
        public void EventAreaService_GetAll_Exc_noEventArea()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                var strException =
                    "Invalid column name";

                // act
                var ex = Assert.Throws<SqlException>(
                                () => _eventAreaService.GetAll());

                // assert
                StringAssert.Contains(strException, ex.Message);
            }
        }
    }
}
