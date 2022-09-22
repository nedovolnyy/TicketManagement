using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace TicketManagement.IntegrationTests.Web
{
    [SetUpFixture]
    internal class TestWebFixture : WebApplicationFactory<Program>
    {
        private IServiceScope _scope;
        internal static IServiceProvider ServiceProvider { get; private set; }
        public static HttpClient Client { get; private set; } = null!;
        public static IConfiguration Configuration { get; set; } = null!;
        private WebApplicationFactory<Program> WebApplicationFactory { get; set; } = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            WebApplicationFactory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.UseSetting("https_port", "7115");
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
