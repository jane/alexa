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

            var builder = new StringBuilder("Today's top deals on Jane are:");

            foreach (var item in storeFrontItems)
            {
                builder.AppendLine($"{item.Title}");
            }
            
            speechResponse.Ssml = builder.ToString();

            var response = ResponseBuilder.Tell(speechResponse);

            return response;
        }
    }
}

