using FluentValidation;
using HealthcareAppointmentManagementAPI.DTO.Patient;

namespace HealthcareAppointmentManagementAPI.Validators
{
    public class PatientValidator : AbstractValidator<CreatePatientDto>
    {
        public PatientValidator()
        {
            RuleFor(p => p.ApplicationUserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future.");
        }
    }
}

