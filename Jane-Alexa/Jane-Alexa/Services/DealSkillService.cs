using System.Collections.Generic;
using System.Threading.Tasks;
using Jane.Alexa.Core;
using Jane.Alexa.Models;
using System.Linq;
using System.Text;
using Alexa.NET;
using Alexa.NET.Response;
using Newtonsoft.Json;

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
		private static string phraseBreaktime = ".5s";

        public DealSkillService(IStoreFrontService storeFrontService)
        {
            _storeFrontService = storeFrontService;
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
			throw new System.NotImplementedException();
		}
	}
}

