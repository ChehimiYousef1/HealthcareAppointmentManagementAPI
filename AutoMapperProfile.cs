using AutoMapper;
using HealthcareAppointmentManagementAPI.DTO.Appointment;
using HealthcareAppointmentManagementAPI.DTO.Doctor;
using HealthcareAppointmentManagementAPI.DTO.Patient;
using HealthcareAppointmentManagementAPI.DTO.Appointment;
using HealthcareAppointmentManagementAPI.DTO.Doctor;
using HealthcareAppointmentManagementAPI.DTO.Patient;
using HealthcareAppointmentManagementAPI.Models;

namespace HealthcareAppointmentManagementAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // =========================
            // Patient Mappings
            // =========================
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();

            // =========================
            // Doctor Mappings
            // =========================
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();

            // =========================
            // Appointment Mappings
            // =========================
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.User.FirstName + " " + src.Patient.User.LastName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.User.FirstName + " " + src.Doctor.User.LastName));

            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<UpdateAppointmentDto, Appointment>();
        }
    }
}

