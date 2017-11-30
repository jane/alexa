using System;
using Jane.Alexa.Models;
using Jane.Alexa.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace Jane.Alexa.Tests.UnitTests
{
    public class HttpClientTests
    {
        [Fact]
        public void GetClient_ReturnsHttpClient()
        {
            var endpoint = "http://fake.endpoint.com";
            var client =
                new HttpClientService(new OptionsWrapper<ConnectionSettings>(
                    new ConnectionSettings() {JaneCatalogEndpoint = endpoint })).Client();

            Assert.Equal(new Uri(endpoint), client.BaseAddress);
        }
    }
}
