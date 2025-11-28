using AutoMapper;
using HealthcareAppointmentManagementAPI.Data;
using HealthcareAppointmentManagementAPI.DTO.Doctor;
using Microsoft.EntityFrameworkCore;
using DoctorModel = HealthcareAppointmentManagementAPI.Models.Doctor;

namespace HealthcareAppointmentManagementAPI.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto)
        {
            var doctor = _mapper.Map<DoctorModel>(dto); // Use alias here
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<DoctorDto> UpdateDoctorAsync(int id, UpdateDoctorDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) throw new KeyNotFoundException("Doctor not found.");

            _mapper.Map(dto, doctor);
            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doctor);
        }
    }
}
