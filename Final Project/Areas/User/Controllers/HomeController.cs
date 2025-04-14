using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {


        public IActionResult ShowNotification()
        {
            return View();
        }

        public IActionResult MyCourses()
        {

            return View();
        }


    }
}
