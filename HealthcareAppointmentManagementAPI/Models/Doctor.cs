namespace HealthcareAppointmentManagementAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Specialty { get; set; }
        public string Biography { get; set; }

        // Claims-like info (optional)
        public string MedicalLicenseNumber { get; set; }

        // Navigation
        public ICollection<Appointment> Appointments { get; set; }
    }
}
