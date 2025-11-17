namespace HealthcareAppointmentManagementAPI.DTO.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        // Shared user info
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Role selected: "Patient" or "Doctor"
        public string Role { get; set; }
    }
}
