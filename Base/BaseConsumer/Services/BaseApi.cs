using BaseConsumer.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BaseConsumer.Services
{
    public class BaseApi : IBaseApi
    {
        private readonly HttpClient _httpClient;

        public BaseApi()
        {
            _httpClient = new HttpClient();
        }

        public void SetUrl(string url)
        {
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task Delete(string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Result> Delete<Result>(string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.DeleteAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task<Result> Delete<Result>(string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.DeleteAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task Get(string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task Get(string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await _httpClient.GetAsync(requestUri, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Result> Get<Result>(string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task<Result> Get<Result>(string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.GetAsync(requestUri, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task Post<T>(T t, string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task Post<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Result> Post<T, Result>(T t, string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task<Result> Post<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.PostAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task Put<T>(T t, string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task Put<T>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(requestUri, t, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Result> Put<T, Result>(T t, string requestUri, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PutAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }

        public async Task<Result> Put<T, Result>(T t, string requestUri, string jwtToken, CancellationToken cancellationToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.PutAsJsonAsync(requestUri, t, cancellationToken);
            return await response.Content.ReadAsAsync<Result>();
        }
    }
}
