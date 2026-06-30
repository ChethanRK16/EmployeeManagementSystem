using EmpManagementSystem.Data;
using EmpManagementSystem.Helpers;
using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmpManagementSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employeeAdd)
        {
            _dbContext.Add(employeeAdd);
            await _dbContext.SaveChangesAsync();
            return employeeAdd;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        //{
        //    return await _dbContext.Employees.ToListAsync();
        //}

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task<PagedResponse<Employee>> GetEmployeesAsync(EmployeeQueryParams query)
        {
            var employeesQuery = _dbContext.Employees.AsQueryable();

            // Search Logic
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                employeesQuery = employeesQuery.Where(e =>
                    e.Name.Contains(query.Search) ||
                    e.Email.Contains(query.Search) ||
                    e.Department.Contains(query.Search));
            }

            // Total count after search
            var totalRecords = await employeesQuery.CountAsync();

            // Pagination
            var employees = await employeesQuery
                .OrderBy(e => e.EmployeeId)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResponse<Employee>
            {
                Data = employees,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)query.PageSize)
            };
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employeeUpdate)
        {
            _dbContext.Update(employeeUpdate);
            await _dbContext.SaveChangesAsync();
            return employeeUpdate;
        }

    }
}
