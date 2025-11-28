using FluentValidation;
using HealthcareAppointmentManagementAPI.DTO.Doctor;

namespace HealthcareAppointmentManagementAPI.Validators
{
    public class DoctorValidator : AbstractValidator<CreateDoctorDto>
    {
        public DoctorValidator()
        {
            RuleFor(d => d.Specialty)
                .NotEmpty().WithMessage("Specialty is required.");
        }
    }
}
