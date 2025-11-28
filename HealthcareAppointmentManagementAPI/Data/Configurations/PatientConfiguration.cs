using HealthcareAppointmentManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareAppointmentManagementAPI.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Gender)
                   .IsRequired();

            // One-to-one: ApplicationUser → Patient
            builder.HasOne(p => p.User)
                   .WithOne(u => u.PatientProfile)
                   .HasForeignKey<Patient>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
