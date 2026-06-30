using AutoMapper;
using EmpManagementSystem.DTOs;
using EmpManagementSystem.Models;

namespace EmpManagementSystem.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}
