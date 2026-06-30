using EmpManagementSystem.DTOs;
using EmpManagementSystem.Models;
using EmpManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {

            if (dto.Username == "admin" && dto.Password == "1234")
            {
                var user = new User
                {
                    Id = 1,
                    Username = dto.Username
                };

                var token = _tokenService.GenerateToken(user);

                return Ok(new { token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
