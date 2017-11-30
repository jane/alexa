using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET;
using Newtonsoft.Json;
using Jane.Alexa.Services;
using System.Threading.Tasks;

namespace Jane.Alexa.Controllers
{
	[Route("api")]
    public class SkillsController : Controller
    {
		private static IDealsService _dealsService;
		public SkillsController(IDealsService dealsService)
		{
			_dealsService = dealsService;
		}

        [HttpPost]
		[Route("deals")]
		[ProducesResponseType(typeof(SkillResponse), 200)]
		public async Task<IActionResult> PostHandleDealSkillRequest([FromBody]SkillRequest skillRequest)
		{
			var storefrontResults = await _dealsService.GetStorefront();
			var speechResponse = new SsmlOutputSpeech();
			speechResponse.Ssml = $"<speak>This is a test.  This is a test.  This is a test.</speak>";
			
			var response = ResponseBuilder.Tell(speechResponse);
			response.Version = JsonConvert.SerializeObject(skillRequest);

			return Ok(response);
		}
    }
}
