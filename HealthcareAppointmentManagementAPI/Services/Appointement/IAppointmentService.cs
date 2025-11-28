using HealthcareAppointmentManagementAPI.DTO.Appointment;

namespace HealthcareAppointmentManagementAPI.Services.Appointement
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto);
        Task<AppointmentDto> UpdateAppointmentAsync(int id, UpdateAppointmentDto dto);
        Task<bool> CancelAppointmentAsync(int id);
    }
}
