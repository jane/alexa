using System.Collections.Generic;
using System.Threading.Tasks;
using Jane.Alexa.Core;
using Jane.Alexa.Models;
using System.Linq;
using System.Text;
using Alexa.NET;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace Jane.Alexa.Services
{
	public interface IDealsService
    {
        Task<SkillResponse> GetStoreFrontSpeachResponse(int take = 5);
		Task<SkillResponse> GetStorefrontDealDetailsForDeal(int sortedDealKey);
    }

    public class DealSkillService : IDealsService
    {
        private readonly ConnectionSettings _connectionSettings;
        private IStoreFrontService _storeFrontService;
		private IMemoryCache _memCache;
		private static string phraseBreaktime = ".5s";

        public DealSkillService(IStoreFrontService storeFrontService, IMemoryCache memCache)
        {
            _storeFrontService = storeFrontService;
			_memCache = memCache;
        }

        public async Task<SkillResponse> GetStoreFrontSpeachResponse(int take = 5)
        {
            var storeFrontItems = await _storeFrontService.GetStorefront(take)
                .ConfigureAwait(false);
            var speechResponse = new SsmlOutputSpeech();

            var builder = new StringBuilder($"<speak>Today's top {take} deals on Jane are:");

			for(int i=0; i < storeFrontItems.Count; i++)
			{
				StorefrontItem item = storeFrontItems[i];
				string andFinal = "";
				if (i == storeFrontItems.Count - 1)
					andFinal = "and ";

				builder.Append($" {andFinal}{item.Title}<break time=\"{phraseBreaktime}\"/> ");
			}

			//close the speech
			builder.Append("</speak>");
            speechResponse.Ssml = builder.ToString();

            var response = ResponseBuilder.Tell(speechResponse);

            return response;
        }

		public Task<SkillResponse> GetStorefrontDealDetailsForDeal(int sortedDealKey)
		{
			StorefrontItem item = _memCache.Get<StorefrontItem>($"{sortedDealKey}");

			if(item == null)
			{
				throw new ValidationException();
			}

			string responseSsml = $"<speak>{item.Title} is {item.Price} and has {item.LikeCount} likes.";
			var speechResponse = new SsmlOutputSpeech()
			{
				Ssml = responseSsml
			};

			return Task.FromResult(ResponseBuilder.Tell(speechResponse));
		}
	}
}

