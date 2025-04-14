using Final_Project.Models;
using Final_Project.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<UserSigin> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SignInManager<UserSigin> signInManager { get; }

        public HomeController(UserManager<UserSigin> userManager, SignInManager<UserSigin> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Login()
        {
            var results = await userManager.GetUserAsync(User);
            TempData["success"] = $"Welocom {results!.FName}";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                if (user.UserType == "admin")
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }


        [Authorize]
        [Authorize(Roles = "User,Teacher")]

        public IActionResult EditProfile()
        {
            return View();
        }
        [Authorize]
        [Authorize(Roles = "User,Teacher")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                user.LName = model.LName;
                user.FName = model.FName;
                user.Address = String.IsNullOrEmpty(model.Address) ? "" : model.Address;
                user.PhoneNumber = String.IsNullOrEmpty(model.PhoneNumber) ? "" : model.PhoneNumber;
                await userManager.UpdateAsync(user);
                await signInManager.RefreshSignInAsync(user);
            }


            return View(model);
        }

        [Authorize]
        [Authorize(Roles = "User,Teacher")]

        public async Task<IActionResult> RemoveImage()
        {
            var user = await userManager.GetUserAsync(User);
            if (String.IsNullOrEmpty(user.Imageurl))
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, user.Imageurl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            user.Imageurl = "";
            await userManager.UpdateAsync(user);


            return RedirectToAction("EditProfile", "Home");
        }
        [Authorize]
        [Authorize(Roles = "User,Teacher")]

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User,Teacher")]
        public async Task<IActionResult> ChangePassword(EditProfileVM model)
        {

            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                var results = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (results.Succeeded)
                {
                    await signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("ProfileView", "Home", new { area = "Admin" });
                }
            }
            return View(model);

        }

    }
}
