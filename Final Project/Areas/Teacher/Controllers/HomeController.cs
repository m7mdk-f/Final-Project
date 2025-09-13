using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class HomeController : Controller
    {



        public IActionResult MyCourses()
        {

            return View();
        }


        public IActionResult ShowNotification()
        {
            return View();
        }


    }
}
