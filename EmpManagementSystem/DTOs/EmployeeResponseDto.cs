namespace EmpManagementSystem.DTOs
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
