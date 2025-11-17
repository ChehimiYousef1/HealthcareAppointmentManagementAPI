namespace HealthcareAppointmentManagementAPI.DTO.Appointment
{
    public class UpdateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
