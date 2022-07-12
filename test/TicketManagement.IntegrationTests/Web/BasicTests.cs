using System.Threading.Tasks;
using NUnit.Framework;

namespace TicketManagement.IntegrationTests.Web
{
    public class BasicTests
    {
        [TestCase("/LayoutsManagement")]
        [TestCase("/VenuesManagement")]
        [TestCase("/UsersManagement")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Act
            var response = await TestWebFixture.Client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
