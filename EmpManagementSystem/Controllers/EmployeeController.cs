using AutoMapper;
using EmpManagementSystem.DTOs;
using EmpManagementSystem.Helpers;
using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagementSystem.Controllers
{
    /// <summary>
    /// APIs for managing employees.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            _logger = logger;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        /*[HttpGet]
        [Route("AllEmployees")]
        public async Task<IActionResult> getAll()
        {
            _logger.LogInformation("Fetching all employees");
            var employees = await _employeeService.GetAllEmployeeAsync();
            var response = _mapper.Map<List<EmployeeResponseDto>>(employees);
            return Ok(response);
        }*/

        /// <summary>
        /// Retrieves a paginated list of employees.
        /// </summary>
        /// <param name="query">Pagination and search parameters.</param>
        /// <returns>List of employees.</returns>
        [HttpGet("Allemployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQueryParams query)
        {
            var result = await _employeeService.GetEmployeesAsync(query);

            return Ok(new PagedResponse<EmployeeResponseDto>
            {
                Data = _mapper.Map<List<EmployeeResponseDto>>(result.Data),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords,
                TotalPages = result.TotalPages
            });
        }

        /// <summary>
        /// Retrieves an employee by Id.
        /// </summary>
        /// <param name="id">Employee Id.</param>
        /// <returns>Employee details.</returns>
        [HttpGet("GetEmpBy/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getById(int id)
        {
            _logger.LogInformation("Fetching employee with Id: {Id}", id);
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                _logger.LogWarning("Employee with Id {Id} not found", id);
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeResponseDto>(employee));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="dto">Employee information.</param>
        /// <returns>Created employee.</returns>
        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            var empCreate = await _employeeService.AddEmployeeAsync(employee);
            var response = _mapper.Map<EmployeeResponseDto>(empCreate);
            return CreatedAtAction(nameof(getById), new { id = response.EmployeeId }, response);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">Employee Id.</param>
        /// <param name="dto">Updated employee information.</param>
        /// <returns>Updated employee.</returns>
        [HttpPut("UpdateEmployee/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var empExist = await _employeeService.GetEmployeeByIdAsync(id);
            if (empExist == null)
            {
                _logger.LogWarning("Employee with Id {Id} not found", id);
                return NotFound();
            }
            _mapper.Map(dto, empExist);
            var empUpdated = await _employeeService.UpdateEmployeeAsync(empExist);
            return Ok(_mapper.Map<EmployeeResponseDto>(empUpdated));
        }

        /// <summary>
        /// Deletes an employee.
        /// </summary>
        /// <param name="id">Employee Id.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("DeleteEmployee/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting employee with Id {Id}", id);
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                _logger.LogWarning("Employee with Id {Id} not found", id);
                return NotFound("Employee Id not found");
            }

            return NoContent();
        }
    }
}
