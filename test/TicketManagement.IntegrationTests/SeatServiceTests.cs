using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class SeatServiceTests
    {
        private SeatService _seatService;

        [SetUp]
        public void Setup()
        {
            _seatService = new SeatService(new SeatRepository());
        }

        [TestCase(1, 2, 3, 5)]
        [TestCase(2, 1, 5, 3)]
        [TestCase(3, 2, 6, 5)]
        public void Insert_WhenInsertSeat_ShouldInt1(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _seatService.Insert(new Seat(id: id, areaId: areaId, row: row, number: number));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, 2, 3, 5)]
        [TestCase(2, 1, 5, 3)]
        [TestCase(3, 2, 6, 5)]
        public void Update_WhenUpdateSeat_ShouldInt1(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _seatService.Update(new Seat(id: id, areaId: areaId, row: row, number: number));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _seatService.Delete(id);

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [Test]
        public void GetAll_WhenHave6Entry_Should6Entry()
        {
            // arrange
            int expectedCount = 10;

            // act
            var actualCount = _seatService.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            int expectedId = 1;

            // act
            var actualId = _seatService.GetById(1);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }
    }
}
