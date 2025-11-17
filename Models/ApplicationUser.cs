using Microsoft.AspNetCore.Identity;

namespace HealthcareAppointmentManagementAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional shared properties for all users
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation properties
        public Patient PatientProfile { get; set; }
        public Doctor DoctorProfile { get; set; }
    }
}
