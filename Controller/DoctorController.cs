using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentManagementAPI.Controller
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
