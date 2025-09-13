using Final_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Final_Project.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserSigin>>();

            string[] roleNames = ["Admin", "User", "Teacher"];

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            string EmailAdmin = "admin@admin.com";
            string adminPassword = "admin123";
            var admin = await userManager.FindByEmailAsync(EmailAdmin);

            string TeacherEmail = "mohamed@gmail.com";
            string TeacherPass = "mohamed123";
            var Teacher = await userManager.FindByEmailAsync(TeacherEmail);

            string UserEmail = "Ahmed@gmail.com";
            string UserPass = "mohamed123";
            var usre = await userManager.FindByEmailAsync(UserEmail);

            if (Teacher is null)
            {
                var TeacherAccount = new UserSigin
                {
                    UserName = TeacherEmail,
                    Email = TeacherEmail,
                    FName = "Teacher",
                    LName = "Teacher",
                    UserType = "Teacher",
                };

                var results = await userManager.CreateAsync(TeacherAccount, TeacherPass);
                if (results.Succeeded)
                {
                    await userManager.AddToRoleAsync(TeacherAccount, "Teacher");
                }
            }

            if (usre is null)
            {
                var UserAccount = new UserSigin
                {
                    UserName = UserEmail,
                    Email = UserEmail,
                    FName = "mohamed",
                    LName = "Ahmed",
                    UserType = "User"
                };
                var results = await userManager.CreateAsync(UserAccount, UserPass);
                if (results.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserAccount, "User");
                }

            }


            if (admin is null)
            {
                var adminUser = new UserSigin
                {
                    UserName = EmailAdmin,
                    Email = EmailAdmin,
                    FName = "admin",
                    LName = "admin",
                    UserType = "admin"
                };

                var createUser = await userManager.CreateAsync(adminUser, adminPassword);

                if (createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
