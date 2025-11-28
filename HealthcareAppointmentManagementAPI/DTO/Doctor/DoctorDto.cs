namespace HealthcareAppointmentManagementAPI.DTO.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Specialty { get; set; }
        public string Biography { get; set; }  // required
        public string MedicalLicenseNumber { get; set; }  // required
        public string Email { get; set; }
    }
}
