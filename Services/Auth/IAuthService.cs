using HealthcareAppointmentManagementAPI.DTO.Auth;

namespace HealthcareAppointmentManagementAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> ExternalLoginAsync(ExternalLoginDto externalLoginDto);
    }
}
