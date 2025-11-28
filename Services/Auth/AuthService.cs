using HealthcareAppointmentManagementAPI.DTO.Auth;
using HealthcareAppointmentManagementAPI.Models;
using HealthcareAppointmentManagementAPI.Token;
using Microsoft.AspNetCore.Identity;

namespace HealthcareAppointmentManagementAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var roles = await _userManager.GetRolesAsync(user);
            return _tokenService.GenerateToken(user, roles);
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new ApplicationException(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, registerDto.Role);
            var roles = await _userManager.GetRolesAsync(user);
            return _tokenService.GenerateToken(user, roles);
        }

        public Task<string> ExternalLoginAsync(ExternalLoginDto externalLoginDto)
        {
            // External login logic goes here (Google/Microsoft)
            throw new NotImplementedException();
        }
    }
}
