using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jane.Alexa.Models;
using Jane.Alexa.Core;

namespace Jane.Alexa.Services
{
    public interface IStoreFrontService
    {
        Task<List<StorefrontItem>> GetStorefront(
            int take = 5,
            bool filterSoldOut = true,
            bool filterEndingSoon = true,
            bool filterIsNew = true);
    }

    public class StoreFrontService : IStoreFrontService
    {
        private readonly IHttpClientService _httpClientService;

        public StoreFrontService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<StorefrontItem>> GetStorefront(
            int take = 5,
            bool filterSoldOut = true,
            bool filterEndingSoon = true,
            bool filterIsNew = true)
        {
            List<StorefrontItem> storefrontResult = await _httpClientService.Client().GetAsync<List<StorefrontItem>>("/storefront").ConfigureAwait(false);

            storefrontResult = filterSoldOut
                ? storefrontResult.Where(sfr => !sfr.IsSoldOut).ToList()
                : storefrontResult;

            storefrontResult = filterEndingSoon
                ? storefrontResult.Where(sfr => !sfr.IsEndingSoon).ToList()
                : storefrontResult;

            storefrontResult = filterIsNew
                ? storefrontResult.Where(sfr => !sfr.IsNew).ToList()
                : storefrontResult;

            storefrontResult = storefrontResult.Take(take).ToList();

            return storefrontResult;
        }
    
    }
}
