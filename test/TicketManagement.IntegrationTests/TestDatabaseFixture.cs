using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.Common.DI;

namespace TicketManagement.IntegrationTests
{
    [SetUpFixture]
    public class TestDatabaseFixture
    {
        public static Configuration Configuration { get; } = new Configuration();

        [OneTimeSetUp]
        public async Task Setup()
        {
            await InitiallizeDatabase();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await DropDatabase();
        }

        public async Task InitiallizeDatabase()
        {
            await DropDatabase();

            var target = new DacpacService();
            target.ProcessDacPac(Configuration.ConnectionString,
                                 "TestTicketManagement.Database",
                                 "TestTicketManagement.Database.dacpac");
        }

        public async Task DropDatabase()
        {
            await Configuration.Container.GetInstance<IDatabaseContext>().Instance.Database.EnsureDeletedAsync();
        }
    }
}
