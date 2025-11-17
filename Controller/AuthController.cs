using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentManagementAPI.Controller
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
