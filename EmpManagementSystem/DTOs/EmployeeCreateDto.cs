using System.ComponentModel.DataAnnotations;

namespace EmpManagementSystem.DTOs
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than zero")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Joining Date is required")]
        public DateTime JoiningDate { get; set; }
    }
}
