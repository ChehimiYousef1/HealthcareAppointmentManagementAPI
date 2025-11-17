using Microsoft.AspNetCore.Authorization;

namespace HealthcareAppointmentManagementAPI.Configurations
{
    public static class AuthorizationPolicies
    {
        // Role-based policies
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireDoctorRole = "RequireDoctorRole";
        public const string RequirePatientRole = "RequirePatientRole";

        // Claims-based policies
        public const string RequireDoctorSpecialty = "RequireDoctorSpecialty";

        public static void AddCustomPolicies(AuthorizationOptions options)
        {
            // Admin-only access
            options.AddPolicy(RequireAdminRole,
                policy => policy.RequireRole("Admin"));

            // Doctor-only access
            options.AddPolicy(RequireDoctorRole,
                policy => policy.RequireRole("Doctor"));

            // Patient-only access
            options.AddPolicy(RequirePatientRole,
                policy => policy.RequireRole("Patient"));

            // Claims-based: allow ALL doctor types (any specialty)
            options.AddPolicy(RequireDoctorSpecialty,
                policy => policy.RequireClaim("specialty"));
        }
    }
}
