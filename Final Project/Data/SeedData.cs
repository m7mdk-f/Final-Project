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

            string[] roleNames = { "Admin", "User", "Techer" };


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

            var user = await userManager.FindByEmailAsync(EmailAdmin);


            if (user == null)
            {
                var adminUser = new UserSigin
                {
                    UserName = EmailAdmin,
                    Email = EmailAdmin,
                    FName = "admin",
                    LName = "admin",
                    Imageurl = "/images/99a2ea67-ff9d-49dd-8de6-86588e0ccdf5profile.jpg",
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
