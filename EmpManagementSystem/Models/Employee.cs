using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpManagementSystem.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
