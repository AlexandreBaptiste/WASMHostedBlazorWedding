using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wedding.Data;
using Wedding.Shared.Models;

namespace Wedding.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jsonFilePath = "Data/Local/Employee.json";
        private readonly DatabaseOptions _dbOptions;
        private readonly List<Employee>? _employees;
        

        public EmployeeController(ApplicationDbContext applicationDbContext, IOptions<DatabaseOptions> dbOptions)
        {
            _context = applicationDbContext;
            _dbOptions = dbOptions.Value;

            if (!_dbOptions.Online)
            {
                _employees = JsonConvert.DeserializeObject<List<Employee>>(System.IO.File.ReadAllText(_jsonFilePath));
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            List<Employee>? employees;

            try
            {
                if (_dbOptions.Online)
                {
                    employees = await _context.Employees.ToListAsync();
                }
                else
                {
                    employees = _employees?.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
            
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            Employee? employee;

            try
            {
                if (_dbOptions.Online)
                {
                    employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                }
                else
                {
                    employee = _employees?.FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok(employee);

        }

        [HttpPatch]
        public async Task<ActionResult> UpdateEmployee(Employee employee)
        {
            Employee? existingEmployee;

            if (employee == null)
            {
                return BadRequest();
            }

            if (_dbOptions.Online)
            {
                existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

                if(existingEmployee == null)
                {
                    return NotFound();
                }
                else
                {
                    existingEmployee.Name = employee.Name;
                    await _context.SaveChangesAsync();
                }    
            }
            else
            {
                existingEmployee = _employees?.FirstOrDefault(x => x.Id == employee.Id);

                if(existingEmployee != null)
                {
                    int? index = _employees?.IndexOf(existingEmployee);

                    if(index != null && index != -1 && _employees != null)
                    {
                        _employees[index.Value] = employee;
                        System.IO.File.WriteAllText(_jsonFilePath, JsonConvert.SerializeObject(_employees, Formatting.Indented));
                    }
                    else
                    {
                        return NotFound(employee.Name);
                    }                       
                }
            }

            return NoContent();
        }
    }
}