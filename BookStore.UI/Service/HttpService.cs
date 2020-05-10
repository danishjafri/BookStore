using BookStore.UI.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.UI.Service
{
    public class HttpService<T> : IHttpService<T> where T : class
    {
        private readonly IHttpClientFactory _client;

        public HttpService(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<bool> Create(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (obj == null)
                return false;
            request.Content = new StringContent(JsonSerializer.Serialize<T>(obj));

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            return false;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1)
                return false;
            var request = new HttpRequestMessage(HttpMethod.Delete, url + id);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;
        }

        public async Task<IList<T>> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IList<T>>(content);
            }
            return null;
        }

        public async Task<T> Get(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(content);
            }
            return null;
        }

        public async Task<bool> Update(string url, T obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            if (obj == null)
                return false;
            request.Content = new StringContent(JsonSerializer.Serialize<T>(obj), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;
        }
    }
}
