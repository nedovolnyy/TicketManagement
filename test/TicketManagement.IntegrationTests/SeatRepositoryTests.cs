using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.DataAccess.IntegrationTests
{
    public class SeatRepositoryTests
    {
        private ISeatRepository _seatRepository;

        [SetUp]
        public void Setup()
        {
            _seatRepository = new SeatRepository();
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(3, 2, 1, 1)]
        public void Insert_WhenInsertSeat_ShouldInt1(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _seatRepository.Insert(new Seat(id: id, areaId: areaId, row: row, number: number));

                // assert
                Assert.AreEqual(expectedResponse, actualResponse);
            }
        }

        [TestCase(1, 1, 1, 1)]
        [TestCase(2, 1, 2, 2)]
        [TestCase(3, 2, 1, 1)]
        public void Update_WhenUpdateSeat_ShouldInt1(int id, int areaId, int row, int number)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expectedResponse = 1;

                // act
                var actualResponse = _seatRepository.Update(new Seat(id: id, areaId: areaId, row: row, number: number));

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
                var actualResponse = _seatRepository.Delete(id);

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
            var actualCount = _seatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // arrange
            int expectedId = 1;

            // act
            var actualId = _seatRepository.GetById(1);

            // assert
            Assert.AreEqual(actualId.Id, expectedId);
        }

        [Test]
        public void GetAllByAreaId_WhenHave5Entry_Should5Entry()
        {
            // arrange
            int expectedCount = 5;

            // act
            var actualCount = _seatRepository.GetAllByAreaId(1).ToList();

            // assert
            Assert.AreEqual(actualCount.Count, expectedCount);
        }
    }
}
