using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BaseConsumer
{
    public abstract class BaseApi
    {
        private readonly string _url;

        public BaseApi(string url)
        {
            _url = url;
        }

        protected async Task Post<T>(T t, string requestUri)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, t);
            response.EnsureSuccessStatusCode();
        }

        protected async Task Post<T>(T t, string requestUri, string jwtToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, t);
            response.EnsureSuccessStatusCode();
        }
    }
}
