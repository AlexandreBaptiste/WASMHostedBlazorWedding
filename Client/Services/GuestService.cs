using Wedding.Interfaces;
using System.Text.Json;
using Wedding.Shared.Models;
using System.Text;

namespace Wedding.Services
{
    public class GuestService : IGuestService
    {
        private readonly HttpClient _httpClient;

        public GuestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Guest>?> Getguests()
        {
            var serverResponse = await _httpClient.GetStreamAsync("api/guest");
            IEnumerable<Guest>? guests = await JsonSerializer.DeserializeAsync<IEnumerable<Guest>>(serverResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return guests;
        }
    }
}