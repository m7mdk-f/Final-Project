using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class HomeController : Controller
    {

        public IActionResult Login()
        {
            TempData["success"] = "Login Successful";

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
