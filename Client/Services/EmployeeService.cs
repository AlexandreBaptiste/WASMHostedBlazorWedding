using Wedding.Interfaces;
using System.Text.Json;
using Wedding.Shared.Models;
using System.Text;

namespace Wedding.Services
{
    public class EmployeeService : IEmployeesService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>?> GetEmployees()
        {
            var serverResponse = await _httpClient.GetStreamAsync("api/employee");
            IEnumerable<Employee>? employees = await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(serverResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return employees;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var serverResponse = await _httpClient.PostAsync("api/employee", new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8,"application/json"));        
            return serverResponse.IsSuccessStatusCode;
        }
    }
}