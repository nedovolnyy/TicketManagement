using System.Linq;
using System.Transactions;
using NUnit.Framework;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Validation;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
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
                int expected = 1;

                // act
                var actual = _seatRepository.Insert(new Seat(id: id, areaId: areaId, row: row, number: number));

                // assert
                Assert.AreEqual(expected, actual);
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
                int expected = 1;

                // act
                var actual = _seatRepository.Update(new Seat(id: id, areaId: areaId, row: row, number: number));

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(2)]
        [TestCase(1)]
        public void Delete_WhenDeleteSeat_ShouldInt1(int id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                // arrange
                int expected = 1;

                // act
                var actual = _seatRepository.Delete(id);

                // assert
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GetAll_WhenHave6Entry_Should6Entry()
        {
            // act
            int exc = 6;

            // actual
            var seats = _seatRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(seats.Count, exc);
        }

        [Test]
        public void GetById_WhenHaveIdEntry_ShouldEntryWithThisId()
        {
            // act
            int exc = 1;

            // actual
            var seat = _seatRepository.GetById(1);

            // assert
            Assert.AreEqual(seat.Id, exc);
        }

        [Test]
        public void GetAllByAreaId_WhenHave5Entry_Should5Entry()
        {
            // act
            int exc = 5;

            // actual
            var seats = _seatRepository.GetAllByAreaId(1).ToList();

            // assert
            Assert.AreEqual(seats.Count, exc);
        }
    }
}
