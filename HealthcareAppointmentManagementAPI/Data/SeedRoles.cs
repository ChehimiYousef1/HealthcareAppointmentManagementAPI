using Microsoft.AspNetCore.Identity;
using HealthcareAppointmentManagementAPI.Models;

namespace HealthcareAppointmentManagementAPI.Data
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            // Define roles for the application
            var roles = new[] { "Admin", "Doctor", "Patient" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }

    public static class SeedUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            // Define users to seed using a simple array of objects
            var users = new[]
            {
                new ApplicationUser { Email = "patient1@test.com", UserName = "patient1@test.com", FirstName = "Patient", LastName = "One" },
                new ApplicationUser { Email = "doctor1@test.com", UserName = "doctor1@test.com", FirstName = "Doctor", LastName = "One" },
                new ApplicationUser { Email = "admin@test.com", UserName = "admin@test.com", FirstName = "Admin", LastName = "User" }
            };

            var roles = new[] { "Patient", "Doctor", "Admin" };

            for (int i = 0; i < users.Length; i++)
            {
                var user = users[i];
                var role = roles[i];

                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}
