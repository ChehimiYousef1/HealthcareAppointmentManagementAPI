using HealthcareAppointmentManagementAPI.Models;

namespace HealthcareAppointmentManagementAPI.Token
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles, IDictionary<string, string> claims = null);
    }
}
