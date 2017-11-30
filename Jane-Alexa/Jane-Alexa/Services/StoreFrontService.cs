using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jane.Alexa.Models;
using Jane.Alexa.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Jane.Alexa.Services
{
    public interface IStoreFrontService
    {
        Task<List<StorefrontItem>> GetStorefront(
            int take = 5);
    }

    public class StoreFrontService : IStoreFrontService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IMemoryCache _cache;

        public StoreFrontService(IHttpClientService httpClientService, IMemoryCache cache)
        {
            _httpClientService = httpClientService;
            _cache = cache;
        }

        public async Task<List<StorefrontItem>> GetStorefront(
            int take = 5)
        {
            List<StorefrontItem> storefrontResult = await _httpClientService.Client().GetAsync<List<StorefrontItem>>("/storefront").ConfigureAwait(false);

            storefrontResult = storefrontResult.Take(take).ToList();

            for (int i = 1; i <= take; i++)
            {
                _cache.Set(i.ToString(), storefrontResult[i - 1]);
            }

            return storefrontResult;
        }
    
    }
}
