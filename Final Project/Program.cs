using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Final_Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDBContext>(optoin =>
            {
                string ConnectionString = builder.Configuration.GetConnectionString("MohamedConnectionString")!;
                optoin.UseSqlServer(ConnectionString);
            });


            builder.Services.AddIdentity<UserSigin, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequiredLength = 8;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            builder.Services.AddTransient<EmailSenderService>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
                options.AddPolicy("RequireTecherRole", policy => policy.RequireRole("Techer"));

            });



            var app = builder.Build();



            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                await SeedData.Initialize(scope.ServiceProvider);
            }

            app.MapAreaControllerRoute(
                 name: "Admin",
                 areaName: "Admin",
                 pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "User",
                areaName: "User",
                pattern: "User/{controller=Home}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
         name: "Techer",
         areaName: "Techer",
         pattern: "Techer/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
