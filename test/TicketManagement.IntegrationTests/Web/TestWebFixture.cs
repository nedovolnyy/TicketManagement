using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace TicketManagement.IntegrationTests.Web
{
    [SetUpFixture]
    internal class TestWebFixture : WebApplicationFactory<Program>
    {
        private IServiceScope _scope;
        internal static IServiceProvider ServiceProvider { get; private set; }
        public static HttpClient Client { get; private set; } = null!;
        private WebApplicationFactory<Program> WebApplicationFactory { get; set; } = null!;
        private IConfiguration Configuration { get; set; } = null!;

        [OneTimeSetUp]
        public void Setup()
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
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            WebApplicationFactory.Dispose();
            Client.Dispose();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
    }
}
