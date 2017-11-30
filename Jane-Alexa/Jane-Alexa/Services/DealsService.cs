﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Jane.Alexa.Core;
using Jane.Alexa.Models;
using Microsoft.Extensions.Options;

namespace Jane.Alexa.Services
{
    public interface IDealsService
    {
        Task<List<object>> GetDeals();

    }

    public class DealsService : IDealsService
    {
        private readonly ConnectionSettings _connectionSettings;
        private IHttpClientService _httpClientService;

        public DealsService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        

        public async Task<List<object>> GetDeals()
        {
            return await _httpClientService.Client().GetAsync<List<object>>("/deals").ConfigureAwait(false);
        }
    }
}