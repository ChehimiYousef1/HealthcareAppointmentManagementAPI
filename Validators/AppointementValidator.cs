using FluentValidation;
using HealthcareAppointmentManagementAPI.DTO.Appointment;

namespace HealthcareAppointmentManagementAPI.Validators
{
    public class AppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.");

            RuleFor(a => a.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.");

            RuleFor(a => a.AppointmentDate)
                .NotEmpty().WithMessage("Appointment date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future.");

            // Removed Status validation as per your request
        }
    }
}
