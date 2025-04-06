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
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(UserManager<UserSigin> userManager, SignInManager<UserSigin> signInManager, IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProfileView()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ProfileView(EditProfileVM model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                user.LName = model.LName;
                user.FName = model.FName;
                user.Address = model.Address is null ? "" : model.Address;
                user.PhoneNumber = model.PhoneNumber is null ? "" : model.PhoneNumber;
                await userManager.UpdateAsync(user);
                await signInManager.RefreshSignInAsync(user);
            }


            return View(model);
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


        [HttpPost]
        public async Task<IActionResult> ChangeImage(EditProfileVM model)
        {
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {

                string FileName = Guid.NewGuid() + model.ImageUrl.FileName;

                using (var stream = new FileStream(Path.Combine(hostEnvironment.WebRootPath, "images", FileName), FileMode.Create))
                {
                    await model.ImageUrl.CopyToAsync(stream);
                };

                var user = await userManager.GetUserAsync(User);
                if (!String.IsNullOrEmpty(user.Imageurl))
                {
                    string filePath = Path.Combine(hostEnvironment.WebRootPath, user.Imageurl.TrimStart('/'));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                user.Imageurl = "/images/" + FileName;
                await userManager.UpdateAsync(user);


                return RedirectToAction("ProfileView", "Home");
            }

            ModelState.AddModelError("ImageUrl", "Please upload a valid image.");
            return View(model);
        }
        public async Task<IActionResult> RemoveImage()
        {
            var user = await userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(user!.Imageurl))
            {
                string filePath = Path.Combine(hostEnvironment.WebRootPath, user.Imageurl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            user!.Imageurl = "";
            await userManager.UpdateAsync(user);


            return RedirectToAction("ProfileView", "Home", new { area = "Admin" });
        }

    }

}
