namespace HealthcareAppointmentManagementAPI.DTO.Doctor
{
    public class CreateDoctorDto
    {
        public string ApplicationUserId { get; set; }

        public string Specialty { get; set; }

        public string Biography { get; set; }  // required field

        public string MedicalLicenseNumber { get; set; }
    }
}
