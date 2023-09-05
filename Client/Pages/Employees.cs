using Microsoft.AspNetCore.Components;
using Wedding.Interfaces;
using Wedding.Shared.Models;

namespace Wedding.Client.Pages
{
    public partial class Employees : ComponentBase
    {
        [Inject]
        public IEmployeesService? EmployeesService { get; set; }

        [Inject]
        public ILogger<Employees>? Logger { get; set; }

        public IEnumerable<Employee> EmployeeList { get; set; } = new List<Employee>();

        protected override async Task OnInitializedAsync()
        {
            if(EmployeesService != null)
            {
                var employees = await EmployeesService.GetEmployees();
                EmployeeList = employees != null && employees.Any() ? employees : new List<Employee>();
            }
            
        }
    }
}