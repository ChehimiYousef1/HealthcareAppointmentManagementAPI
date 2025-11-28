using HealthcareAppointmentManagementAPI.Data;
using HealthcareAppointmentManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareAppointmentManagementAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.PatientProfile)
                .Include(u => u.DoctorProfile)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.UserName,
                    u.FirstName,
                    u.LastName,
                    Patient = u.PatientProfile != null ? new
                    {
                        u.PatientProfile.Id,
                        u.PatientProfile.DateOfBirth
                    } : null,
                    Doctor = u.DoctorProfile != null ? new
                    {
                        u.DoctorProfile.Id,
                        u.DoctorProfile.Specialty,
                        u.DoctorProfile.Biography,
                        u.DoctorProfile.MedicalLicenseNumber
                    } : null
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
