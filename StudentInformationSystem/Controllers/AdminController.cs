using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Layout"] = "~/Views/Shared/_AdminLayout.cshtml";
            return View();
        }
    }

}
