using NUnit.Framework;

namespace TicketManagement.IntegrationTests.Web
{
    public class BasicTests
    {
        [TestCase("/AreasManagement")]
        [TestCase("/LayoutsManagement/")]
        [TestCase("/Identity/Account/Login")]
        [TestCase("/Identity/Account/Register")]
        [TestCase("/UsersManagement")]
        [TestCase("/VenuesManagement")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Act
            var response = await TestWebFixture.Client.GetAsync(url);

            // Assert
            if (TestWebFixture.Configuration.GetValue<bool>("UseReact"))
            {
                Assert.Pass(); // Miss out for ReactJS case
            }
            else
            {
                response.EnsureSuccessStatusCode(); // Status Code 200-299
                Assert.AreEqual("text/html; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }
        }
    }
}
