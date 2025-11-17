using AutoMapper;
using HealthcareAppointmentManagementAPI.Data;
using HealthcareAppointmentManagementAPI.DTO.Patient;
using HealthcareAppointmentManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using PatientModel = HealthcareAppointmentManagementAPI.Models.Patient;

namespace HealthcareAppointmentManagementAPI.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
        {
            var patient = _mapper.Map<PatientModel>(dto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) throw new KeyNotFoundException("Patient not found.");

            _mapper.Map(dto, patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientDto>(patient);
        }
    }
}
