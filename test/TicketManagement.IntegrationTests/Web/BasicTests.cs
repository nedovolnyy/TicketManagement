using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.MVC.Helpers;

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
