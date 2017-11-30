using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Response;
using Jane.Alexa.Services;
using Jane.Alexa.Tests.Mocks;
using Xunit;
using Microsoft.Extensions.Caching.Memory;

namespace Jane.Alexa.Tests.UnitTests
{
    public class DealSkillTests
    {
        [Fact]
        public async Task GettingDealfrontResponse_ReturnsSkillResponse()
        {
            var service = new DealSkillService(new StoreFrontServiceMock(), new MemoryCache(new MemoryCacheOptions()));

            var response = await service.GetStoreFrontSpeachResponse().ConfigureAwait(false);

            var output = (SsmlOutputSpeech) response.Response.OutputSpeech;

            Assert.Contains("Today's top 5 deals on Jane are", output.Ssml);
        }
    }
}
