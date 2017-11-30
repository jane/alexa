using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Jane.Alexa.Models;
using Microsoft.Extensions.Options;

namespace Jane.Alexa.Services
{

    public interface IHttpClientService
    {
        HttpClient Client();
    }

    public class HttpClientService
    {
        private readonly ConnectionSettings _connectionSettings;

        public HttpClientService(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        public HttpClient Client()
        {
            var client = new HttpClient { BaseAddress = new Uri(_connectionSettings.JaneCatalogEndpoint) };
            return client;
        }

    }
}
