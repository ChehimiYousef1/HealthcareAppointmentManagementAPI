namespace HealthcareAppointmentManagementAPI.DTO.Patient
{
    public class CreatePatientDto
    {
        public string ApplicationUserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
