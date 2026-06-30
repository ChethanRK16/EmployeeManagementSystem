using EmpManagementSystem.Helpers;
using EmpManagementSystem.Models;
using EmpManagementSystem.Repository;
using System;

namespace EmpManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employeeAdd)
        {
            return await _employeeRepository.AddEmployeeAsync(employeeAdd);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }

        //public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        //{
        //    return await _employeeRepository.GetAllEmployeeAsync();
        //}

        public async Task<PagedResponse<Employee>> GetEmployeesAsync(EmployeeQueryParams query)
        {
            return await _employeeRepository.GetEmployeesAsync(query);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employeeUpdate)
        {
            return await _employeeRepository.UpdateEmployeeAsync(employeeUpdate);
        }

    }
}
