using Wedding.Interfaces;
using System.Text.Json;
using Wedding.Shared.Models;

namespace Wedding.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Account>?> GetAccounts()
        {
            var serverResponse = await _httpClient.GetStreamAsync("api/account");
            IEnumerable<Account>? accounts = await JsonSerializer.DeserializeAsync<IEnumerable<Account>>(serverResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return accounts;
        }
    }
}