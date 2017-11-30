using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET;
using Newtonsoft.Json;

namespace Jane.Alexa.Controllers
{
    [Route("api/deals")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpPost]
		[ProducesResponseType(typeof(SkillResponse), 200)]
		public IActionResult PostHandleSkillRequest(SkillRequest skillRequest)
		{
			var speechResponse = new SsmlOutputSpeech();
			speechResponse.Ssml = $"<speak>This is a test.  This is a test.  This is a test.</speak>";

			var response = ResponseBuilder.Tell(speechResponse);
			response.Version = JsonConvert.SerializeObject(skillRequest);

			return Ok(response);
		}
    }
}
