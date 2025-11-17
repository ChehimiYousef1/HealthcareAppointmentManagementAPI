using AutoMapper;
using HealthcareAppointmentManagementAPI.Data;
using HealthcareAppointmentManagementAPI.DTO.Appointment;
using HealthcareAppointmentManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using AppointmentModel = HealthcareAppointmentManagementAPI.Models.Appointment;

namespace HealthcareAppointmentManagementAPI.Services.Appointement
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            var appointment = _mapper.Map<AppointmentModel>(dto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDto> UpdateAppointmentAsync(int id, UpdateAppointmentDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) throw new KeyNotFoundException("Appointment not found.");

            _mapper.Map(dto, appointment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<bool> CancelAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            appointment.Status = "Canceled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
