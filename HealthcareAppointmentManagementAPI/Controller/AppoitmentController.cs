using HealthcareAppointmentManagementAPI.DTO.Appointment;
using HealthcareAppointmentManagementAPI.Services.Appointement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthcareAppointmentManagementAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient,Doctor,Admin")] // Require authentication and roles
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/appointment/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound(); // 404 if not found

            return Ok(appointment); // 200 OK
        }

        // GET: api/appointment
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments); // 200 OK
        }

        // POST: api/appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 Bad Request if model invalid

            var appointment = await _appointmentService.CreateAppointmentAsync(dto);

            // Return 201 Created with route to new resource
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        // PUT: api/appointment/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = await _appointmentService.UpdateAppointmentAsync(id, dto);

            if (appointment == null)
                return NotFound(); // 404 if not found

            return Ok(appointment); // 200 OK
        }

        // DELETE: api/appointment/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);

            if (!result)
                return NotFound(); // 404 if not found

            return NoContent(); // 204 No Content
        }
    }
}
