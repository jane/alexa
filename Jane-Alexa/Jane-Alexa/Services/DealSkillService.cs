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
        Task<SkillResponse> GetStoreFrontSpeachResponse(int take = 5, bool filterSoldOut = true, bool filterEndingSoon = true, bool filterIsNew = true);
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

        public async Task<SkillResponse> GetStoreFrontSpeachResponse(int take = 5,
            bool filterSoldOut = true,
            bool filterEndingSoon = true,
            bool filterIsNew = true)
        {
            var storeFrontItems = await _storeFrontService.GetStorefront(take, filterSoldOut, filterEndingSoon, filterIsNew)
                .ConfigureAwait(false);
            var speechResponse = new SsmlOutputSpeech();

            var builder = new StringBuilder("<speak>Today's top deals on Jane are:");

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
    }
}

