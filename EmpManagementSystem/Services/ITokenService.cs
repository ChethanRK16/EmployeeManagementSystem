using EmpManagementSystem.Models;

namespace EmpManagementSystem.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
