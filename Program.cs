// Import necessary namespaces
using AutoMapper; // AutoMapper for DTO ↔ Model mapping
using FluentValidation; // FluentValidation core
using FluentValidation.AspNetCore; // Integration of FluentValidation with ASP.NET Core
using HealthcareAppointmentManagementAPI.Data; // ApplicationDbContext
using HealthcareAppointmentManagementAPI.Models;
using HealthcareAppointmentManagementAPI.Services.Appointement; // Appointment services
using HealthcareAppointmentManagementAPI.Services.Auth; // Auth services
using HealthcareAppointmentManagementAPI.Services.Doctor; // Doctor service interfaces/implementations
using HealthcareAppointmentManagementAPI.Services.Patient; // Patient service interfaces/implementations
using HealthcareAppointmentManagementAPI.Token;
using HealthcareAppointmentManagementAPI.Validators; // Custom FluentValidation validators
using Microsoft.AspNetCore.Identity; // For Identity and user management
using Microsoft.EntityFrameworkCore; // For EF Core DbContext

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Add services to the container
// -----------------------------

// Add controllers and configure FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // Automatically register all validators in the assembly (Patient, Doctor, Appointment)
        fv.RegisterValidatorsFromAssemblyContaining<PatientValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<DoctorValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<AppointmentValidator>();

        // Disable default DataAnnotations validation (optional)
        fv.DisableDataAnnotationsValidation = true;
    });

// -----------------------------
// Swagger / OpenAPI
// -----------------------------
builder.Services.AddEndpointsApiExplorer(); // Adds endpoint API explorer
builder.Services.AddSwaggerGen();           // Adds Swagger generator for API documentation

// -----------------------------
// Database (EF Core) - Example
// -----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // SQL Server connection

// -----------------------------
// Identity configuration
// -----------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// -----------------------------
// JWT Authentication setup (example placeholder)
// -----------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer("Bearer", options =>
{
    // Configure JWT token validation parameters here
    // options.TokenValidationParameters = ...
});

// -----------------------------
// AutoMapper
// -----------------------------
builder.Services.AddAutoMapper(typeof(Program)); // Registers AutoMapper using the AutoMapperProfile

// -----------------------------
// Dependency Injection for Services
// -----------------------------
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// -----------------------------
// Configure HTTP request pipeline
// -----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // Enable Swagger in development
    app.UseSwaggerUI();     // Enable Swagger UI for interactive API testing
}

app.UseHttpsRedirection(); // Enforce HTTPS

// -----------------------------
// Authentication & Authorization Middlewares
// -----------------------------
app.UseAuthentication(); // Enable authentication (JWT, Identity)
app.UseAuthorization();  // Enable authorization (roles, policies)

// -----------------------------
// Map Controllers
// -----------------------------
app.MapControllers(); // Map API endpoints to controllers

// -----------------------------
// Run the application
// -----------------------------
app.Run();
