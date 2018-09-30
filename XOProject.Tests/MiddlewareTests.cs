using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net;

namespace XOProject.Tests
{
    public class MiddlewareTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public MiddlewareTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>().UseEnvironment("Development"));
            _client = _server.CreateClient();
        }

        [Test]
        public async Task Index_Get_ReturnsIndexHtmlPage()
        {
            var response = await _client.GetAsync("/");

            HttpStatusCode responseMessage = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsEmpty(responseString);
            _client.Dispose();
            _server.Dispose();
        }
    }
}
