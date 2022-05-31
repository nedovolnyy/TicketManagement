using System;
using NUnit.Framework;
using TicketManagement.DataAccess.ADO;
using TicketManagement.DataAccess.Interfaces;

namespace TicketManagement.IntegrationTests
{
    /// <summary>
    /// The purpose of this class is just a demonstration.
    /// Please refer to the documentation link for more information regarding NUnit framework.
    /// https://docs.nunit.org/articles/nunit/intro.html.
    /// </summary>
    [SetUpFixture]
    public abstract class IntegrationTestBase : IDisposable
    {
        private IDatabaseContext _testDbContext;

        [SetUp]
        public void Setup()
        {
            try
            {
                SetupLocalBD.SetupLocalDb();
                _testDbContext = new DatabaseContext();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        [TearDown]
        public void TearDown()
        {
            _testDbContext.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _testDbContext.Dispose();
        }
    }
}