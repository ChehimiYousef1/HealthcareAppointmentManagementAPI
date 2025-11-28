using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthcareAppointmentManagementAPI.Configurations;
using HealthcareAppointmentManagementAPI.Data;
using HealthcareAppointmentManagementAPI.Models;
using HealthcareAppointmentManagementAPI.Services.Appointement;
using HealthcareAppointmentManagementAPI.Services.Auth;
using HealthcareAppointmentManagementAPI.Services.Doctor;
using HealthcareAppointmentManagementAPI.Services.Patient;
using HealthcareAppointmentManagementAPI.Token;
using HealthcareAppointmentManagementAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Add services to the container
// -----------------------------
builder.Services.AddControllers();

// -----------------------------
// Enable CORS
// -----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()   // Use specific origins in production
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// -----------------------------
// FluentValidation
// -----------------------------
builder.Services.AddValidatorsFromAssemblyContaining<PatientValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DoctorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AppointmentValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// -----------------------------
// Database (EF Core)
// -----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------
// Identity configuration
// -----------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// -----------------------------
// JWT Authentication
// -----------------------------
// Bind JwtOptions from configuration
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt")
);

var jwtOpts = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtOptions>();

if (jwtOpts == null || string.IsNullOrEmpty(jwtOpts.SecretKey))
    throw new Exception("Jwt:SecretKey is missing in appsettings.json");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOpts.Issuer,
        ValidAudience = jwtOpts.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOpts.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});



// -----------------------------
// AutoMapper
// -----------------------------
builder.Services.AddAutoMapper(typeof(Program));

// -----------------------------
// Dependency Injection for Services
// -----------------------------
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// -----------------------------
// Swagger / OpenAPI with JWT
// -----------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// -----------------------------
// Seed roles and users (after app is built)
// -----------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    SeedRoles.SeedAsync(roleManager).GetAwaiter().GetResult();
    SeedUsers.SeedAsync(userManager).GetAwaiter().GetResult();
}


// -----------------------------
// Configure HTTP request pipeline
// -----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ Enable CORS BEFORE authentication/authorization
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
