using Final_Project.Models;
using Final_Project.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<UserSigin> userManager;
        private readonly SignInManager<UserSigin> signInManager;

        public HomeController(UserManager<UserSigin> userManager, SignInManager<UserSigin> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProfileView()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                UserSigin user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var results = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (results.Succeeded)
                {
                    TempData["SuccessMessage"] = "Your password has been changed successfully!";
                    await signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Invalid Current Password");
                }

            }
            return View(model);
        }


    }

}
