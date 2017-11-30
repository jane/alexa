using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET;
using Newtonsoft.Json;
using Jane.Alexa.Services;
using System.Threading.Tasks;
using System;
using Alexa.NET.Request.Type;
using System.ComponentModel.DataAnnotations;

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
			try
			{
				SkillResponse response = null;
				if (skillRequest.Request.Type == "IntentRequest")
				{
					IntentRequest ir = skillRequest.Request as IntentRequest;
					string intentName = ir.Intent.Name;
					if(intentName == "Deals")
					{
						//get the dealTake number, assume a default if no dealTake number was supplied.
						int dealTakeParse = 5;
						Int32.TryParse(ir.Intent.Slots["dealTake"].Value, out dealTakeParse);
						int? dealTakeNumber = dealTakeParse;

						response = await _dealsService.GetStoreFrontSpeachResponse(dealTakeParse);
					}
					else if(intentName == "DealDetail")
					{
						//we need to snag the item that the user asked for as a requirement
						int sortedDealKey = 0;
						if (!Int32.TryParse(ir.Intent.Slots["sortedDealNumber"].Value, out sortedDealKey))
							throw new ValidationException();

						//if we got the key, we can ask the service for the SkillResponse related to this deal by the sortedDealKey
						response = await _dealsService.GetStorefrontDealDetailsForDeal(sortedDealKey);
					}
				}

				return Ok(response);
			}
			catch(Exception ex)
			{
				return Ok(CreateErrorSkillResponse());
			}

		}

		private static SkillResponse CreateErrorSkillResponse()
		{
			return new SkillResponse();
		}
    }
}
