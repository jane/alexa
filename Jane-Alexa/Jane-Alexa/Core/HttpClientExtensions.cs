using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jane.Alexa.Core
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            var response = await client.GetAsync(requestUri).ConfigureAwait(false);
            var whatever = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = content;

            var response = await client.SendAsync(request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        }
    }
}
