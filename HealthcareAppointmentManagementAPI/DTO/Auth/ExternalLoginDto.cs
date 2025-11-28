namespace HealthcareAppointmentManagementAPI.DTO.Auth
{
    public class ExternalLoginDto
    {
        public string Provider { get; set; }   // Google / Microsoft
        public string IdToken { get; set; }    // OAuth token
    }
}
