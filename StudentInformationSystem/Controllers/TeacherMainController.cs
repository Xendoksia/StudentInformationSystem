using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherMainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}