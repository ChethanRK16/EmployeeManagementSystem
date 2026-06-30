using EmpManagementSystem.Helpers;
using EmpManagementSystem.Models;

namespace EmpManagementSystem.Repository
{
    public interface IEmployeeRepository
    {
        //Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task<PagedResponse<Employee>> GetEmployeesAsync(EmployeeQueryParams query);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employeeAdd);
        Task<Employee> UpdateEmployeeAsync(Employee employeeUpdate);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
