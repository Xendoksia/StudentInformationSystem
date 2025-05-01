using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentCalendarController : Controller
    {
        public IActionResult AcademicCalendar()
        {
            ViewBag.IdentityNumber = HttpContext.Session.GetString("IdentityNumber");
            return View();
        }
    }
}
