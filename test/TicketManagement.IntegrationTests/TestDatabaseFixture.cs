using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using TicketManagement.Common.DI;

namespace TicketManagement.IntegrationTests
{
    [SetUpFixture]
    internal class TestDatabaseFixture : WebApplicationFactory<Program>
    {
        private IServiceScope _scope;
        internal static IServiceProvider ServiceProvider { get; private set; }
        internal static IDatabaseContext DatabaseContext { get; private set; }
        private WebApplicationFactory<Program> WebApplicationFactory { get; set; } = null!;
        protected HttpClient Client { get; private set; } = null!;
        private IConfiguration Configuration { get; set; } = null!;

        [OneTimeSetUp]
        public async Task Setup()
        {
            WebApplicationFactory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureLogging(p => p.AddFilter(logLevel => logLevel >= LogLevel.Warning));
            builder.ConfigureServices(services =>
            {
            });
        });
            Client = WebApplicationFactory.CreateClient();
            Configuration = WebApplicationFactory.Services.GetRequiredService<IConfiguration>();
            _scope = WebApplicationFactory.Services.CreateScope();
            ServiceProvider = _scope.ServiceProvider;
            DatabaseContext = ServiceProvider.GetRequiredService<IDatabaseContext>();

            AssertionOptions.FormattingOptions.MaxLines = 500;
            await InitiallizeDatabase();
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await DropDatabase();
            WebApplicationFactory.Dispose();
            Client.Dispose();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }

        public async Task InitiallizeDatabase()
        {
            await DropDatabase();

            var target = new DacpacService();
            target.ProcessDacPac(DatabaseContext.ConnectionString,
                                 Configuration["DatabaseName:DefaultDatabaseName"],
                                 Configuration["DatabaseFileName:DefaultDatabaseFileName"]);
        }

        public async Task DropDatabase()
        {
            await DatabaseContext.Instance.Database.EnsureDeletedAsync();
        }
    }
}
