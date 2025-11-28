using HealthcareAppointmentManagementAPI.DTO.Patient;

namespace HealthcareAppointmentManagementAPI.Services.Patient
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
        Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto dto);
    }
}
