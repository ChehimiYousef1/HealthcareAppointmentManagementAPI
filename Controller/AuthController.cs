using HealthcareAppointmentManagementAPI.DTO.Auth;
using HealthcareAppointmentManagementAPI.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var token = await _authService.RegisterAsync(registerDto);
            return Ok(new { Token = token });
        }

        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto externalLoginDto)
        {
            var token = await _authService.ExternalLoginAsync(externalLoginDto);
            return Ok(new { Token = token });
        }
    }
}
