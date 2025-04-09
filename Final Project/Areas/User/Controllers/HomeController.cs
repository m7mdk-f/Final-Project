using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
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

        public IActionResult ShowNotification()
        {
            return View();
        }


    }
}
