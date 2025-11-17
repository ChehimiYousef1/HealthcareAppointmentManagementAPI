using HealthcareAppointmentManagementAPI.DTO.Doctor;

namespace HealthcareAppointmentManagementAPI.Services.Doctor
{
    public interface IDoctorService
    {
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto);
        Task<DoctorDto> UpdateDoctorAsync(int id, UpdateDoctorDto dto);
    }
}
