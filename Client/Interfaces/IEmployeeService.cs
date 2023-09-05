using System.Threading.Tasks;
using Wedding.Shared.Models;

namespace Wedding.Interfaces
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employee>?> GetEmployees();

        Task<bool> UpdateEmployee(Employee employee);
    }
}
