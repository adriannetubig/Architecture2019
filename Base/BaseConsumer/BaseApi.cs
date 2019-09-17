using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BaseConsumer.Services
{
    public abstract class BaseApi
    {
        private readonly string _url;

        public BaseApi(string url)
        {
            _url = url;
        }

        protected async Task Post<T>(T t, string requestUri, CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        protected async Task Post<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        protected async Task<Result> Post<T, Result>(T t, string requestUri, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        protected async Task<Result> Post<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }
    }
}
