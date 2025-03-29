using Final_Project.Models;
using Final_Project.ModelView;
using Final_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Text.Encodings.Web;

namespace Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserSigin> userManager;
        private readonly SignInManager<UserSigin> signInManager;
        private readonly EmailSenderService _emailSender;


        public AccountController(UserManager<UserSigin> userManager,SignInManager<UserSigin> signInManager,EmailSenderService emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._emailSender = emailSender;
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginMV model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }

                ModelState.AddModelError("All", "Invalid email or password");
            }

            return View(model);
        }
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterMV model)
        {
            if (ModelState.IsValid) 
            { 
                var user =new UserSigin()
                {
                    Email = model.Email,
                    UserName=model.Email,
                    FName=model.FName,
                    LName=model.LName,
                };

                if (model.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Use 8 or more characters for your password.");
                    return View(model);
                }

                var check = await userManager.FindByEmailAsync(user.Email);
                if (check != null)
                {
                    ModelState.AddModelError("Email", "Email Is Exist");

                    return View(model);
                }

                var results = await userManager.CreateAsync(user, model.Password);

                if (!results.Succeeded) {
                    foreach (var item in results.Errors)
                    {
                         ModelState.AddModelError("Password", item.Description);
                       
                    }
                    return View(model);
                }

                await signInManager.SignInAsync(user,false);
                return RedirectToAction("index", "home" ,new { area = "User" });
            }
           
            return View(model);
        }
        public async Task<IActionResult> SetImageProfile()
        {
            return View();
        }
        public async Task<IActionResult> Siginout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM model)
        {
            if (ModelState.IsValid) { 
                var user=await userManager.FindByEmailAsync(model.Email);

                if (user != null) {
                    await SendForgotPasswordEmail(user.Email, user);
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");

                }
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string Token, string Email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (Token == null || Email == null)
            {
                ViewBag.ErrorTitle = "Invalid Password Reset Token";
                ViewBag.ErrorMessage = "The Link is Expired or Invalid";
                return View("Error");
            }
            else
            {
                ResetPasswordVM model = new ResetPasswordVM();
                model.Email = Email;
                model.Token = Token;
                return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPass);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ResetPasswordConfirmation", "Account");
                    }


                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description); 
                    }

                    return View(model);
                }


                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

         
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        private async Task SendForgotPasswordEmail(string? email, UserSigin user)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action("ResetPassword", "Account",
                new { Email = email, Token = token }, protocol: HttpContext.Request.Scheme);

            var safeLink = HtmlEncoder.Default.Encode(passwordResetLink);

            var subject = "Reset Your Password";

            var messageBody = $@"
            <div style=""font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: #333; line-height: 1.5; padding: 20px;"">
                <h2 style=""color: #007bff; text-align: center;"">Password Reset Request</h2>
                <p style=""margin-bottom: 20px;"">Hi {user.FName} {user.LName},</p>
        
                <p>We received a request to reset your password for your <strong>Dot Net Tutorials</strong> account. If you made this request, please click the button below to reset your password:</p>
        
                <div style=""text-align: center; margin: 20px 0;"">
                    <a href=""{safeLink}"" 
                       style=""background-color: #007bff; color: #fff; padding: 10px 20px; text-decoration: none; font-weight: bold; border-radius: 5px; display: inline-block;"">
                        Reset Password
                    </a>
                </div>
        
                <p>If the button above doesn’t work, copy and paste the following URL into your browser:</p>
                <p style=""background-color: #f8f9fa; padding: 10px; border: 1px solid #ddd; border-radius: 5px;"">
                    <a href=""{safeLink}"" style=""color: #007bff; text-decoration: none;"">{safeLink}</a>
                </p>
        
                <p>If you did not request to reset your password, please ignore this email or contact support if you have concerns.</p>
        
                <p style=""margin-top: 30px;"">Thank you,<br />The Dot Net Tutorials Team</p>
            </div>";

            // Send the email
            await _emailSender.SendEmailAsync(email, subject, messageBody,true);
        }
    }

}
