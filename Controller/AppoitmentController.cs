using Microsoft.AspNetCore.Mvc;

namespace HealthcareAppointmentManagementAPI.Controller
{
    public class AppoitmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
