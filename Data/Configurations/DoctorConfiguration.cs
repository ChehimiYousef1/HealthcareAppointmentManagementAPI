using HealthcareAppointmentManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareAppointmentManagementAPI.Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Specialty)
                   .IsRequired()
                   .HasMaxLength(100);

            // One-to-one: ApplicationUser → Doctor
            builder.HasOne(d => d.User)
                   .WithOne(u => u.DoctorProfile)
                   .HasForeignKey<Doctor>(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
