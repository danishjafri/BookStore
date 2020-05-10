using BookStore.Shared;
using BookStore.UI.Contracts;
using BookStore.UI.Helpers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.UI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _client;

        public AuthService(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<bool> Register(UserRegisterDto userRegisterDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, EndPoints.Registration);
            request.Content = new StringContent(JsonSerializer.Serialize(userRegisterDto), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
