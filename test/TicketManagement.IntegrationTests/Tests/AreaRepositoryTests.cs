using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Interfaces;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class AreaRepositoryTests
    {
        private IAreaRepository _areaRepository;

        [SetUp]
        public void Setup()
        {
            _areaRepository = new AreaRepository();
        }

        [Test]
        public void Area_GetAll()
        {
            // act
            int exc = 3;

            // actual
            var areas = _areaRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(areas.Count, exc);
        }

        [Test]
        public void Area_GetById()
        {
            // act
            int exc = 1;

            // actual
            var area = _areaRepository.GetById(1);

            // assert
            Assert.AreEqual(area.Id, exc);
        }

        [Test]
        public void Area_GetAllByLayoutId()
        {
            // act
            int exc = 2;

            // actual
            var areas = _areaRepository.GetAllByLayoutId(1).ToList();

            // assert
            Assert.AreEqual(areas.Count, exc);
        }
    }
}
