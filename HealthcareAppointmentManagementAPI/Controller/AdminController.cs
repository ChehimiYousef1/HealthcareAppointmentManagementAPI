using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentManagementAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Models.ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<Models.ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromQuery] string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return Ok(new { Role = roleName });
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromQuery] string userId, [FromQuery] string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found.");

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return Ok(new { User = user.Email, Role = roleName });
        }
    }
}
