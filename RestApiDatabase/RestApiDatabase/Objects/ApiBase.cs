using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestApiDatabase.Objects
{
    public abstract class ApiBase
    {
        public ApiBase(Uri uri)
        {
            Url = uri;
        }

        public Uri Url { get; private set; }

        public T Get<T>(string relativeUrl)
        {
            using var client = new HttpClient
            {
                BaseAddress = Url
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Log.TestLogger.Info("Execute Get Request. Base URL: \"{0}\", relative URL: \"{1}\"", Url, relativeUrl);

            var response = client.GetAsync(relativeUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
            throw new Exception("Generic Error");
        }
    }
}
