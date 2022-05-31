using System.Linq;
using NUnit.Framework;
using TicketManagement.DataAccess.Repositories;

namespace TicketManagement.IntegrationTests.Tests
{
    public class LayoutRepositoryTests
    {
        private LayoutRepository _layoutRepository;

        [SetUp]
        public void Setup()
        {
            _layoutRepository = new LayoutRepository();
        }

        [Test]
        public void Layout_GetAll()
        {
            // act
            int exc = 7;

            // actual
            var layouts = _layoutRepository.GetAll().ToList();

            // assert
            Assert.AreEqual(layouts.Count, exc);
        }

        [Test]
        public void Layout_GetById()
        {
            // act
            int exc = 1;

            // actual
            var layout = _layoutRepository.GetById(1);

            // assert
            Assert.AreEqual(layout.Id, exc);
        }

        [Test]
        public void Layout_GetAllByLayoutId()
        {
            // act
            int exc = 2;

            // actual
            var layouts = _layoutRepository.GetAllByVenueId(1).ToList();

            // assert
            Assert.AreEqual(layouts.Count, exc);
        }
    }
}
