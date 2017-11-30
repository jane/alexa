using System.Collections.Generic;
using System.Threading.Tasks;
using Jane.Alexa.Core;
using Jane.Alexa.Models;
using System.Linq;

namespace Jane.Alexa.Services
{
	public interface IDealsService
    {
        Task<List<StorefrontItem>> GetStorefront(int take = 5, bool filterSoldOut = true, bool filterEndingSoon = true, bool filterIsNew = true);
    }

    public class DealSkillService : IDealsService
    {
        private readonly ConnectionSettings _connectionSettings;
        private IHttpClientService _httpClientService;

        public DealSkillService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        
        public async Task<List<StorefrontItem>> GetStorefront(
			int take = 5, 
			bool filterSoldOut = true, 
			bool filterEndingSoon = true, 
			bool filterIsNew = true)
        {
            List<StorefrontItem> storefrontResult =  await _httpClientService.Client().GetAsync<List<StorefrontItem>>("/storefront").ConfigureAwait(false);

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

