using AutoMapper;
using HealthcareAppointmentManagementAPI.DTO.Patient;
using HealthcareAppointmentManagementAPI.DTO.Doctor;
using HealthcareAppointmentManagementAPI.DTO.Appointment;
using HealthcareAppointmentManagementAPI.Models;

namespace HealthcareAppointmentManagementAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Patient mappings
            CreateMap<CreatePatientDto, Patient>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ApplicationUserId));
            CreateMap<UpdatePatientDto, Patient>();
            CreateMap<Patient, PatientDto>();

            // Doctor mappings
            CreateMap<CreateDoctorDto, Doctor>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ApplicationUserId));
            CreateMap<UpdateDoctorDto, Doctor>();
            CreateMap<Doctor, DoctorDto>();

            // Appointment mappings
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<UpdateAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
