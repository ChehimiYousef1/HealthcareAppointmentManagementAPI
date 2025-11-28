namespace HealthcareAppointmentManagementAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        // Navigation
        public ICollection<Appointment> Appointments { get; set; }
    }
}
