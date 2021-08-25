using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeaInk.Endpoints.Client.Client
{
    public class ClientBase
    {
        protected readonly HttpClient Client;
        protected readonly JsonSerializerOptions JsonSerializerOptions;
        
        public ClientBase(HttpClient client, JsonSerializerOptions jsonSerializerOptions)
        {
            JsonSerializerOptions = jsonSerializerOptions;
            Client = client;
        }

        protected async Task<T> GetValueAsync<T>(string uri)
        {
            HttpResponseMessage response = await Client.GetAsync(uri);

            if (response.StatusCode is not HttpStatusCode.OK)
                throw new IOException($"{response.StatusCode.ToString()} {response.ReasonPhrase}");

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions) ?? default(T);
        }
    }
}