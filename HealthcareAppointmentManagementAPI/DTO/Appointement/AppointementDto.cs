namespace HealthcareAppointmentManagementAPI.DTO.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        // Display-friendly fields
        public string PatientName { get; set; }   // Added
        public string DoctorName { get; set; }    // Added
    }
}
