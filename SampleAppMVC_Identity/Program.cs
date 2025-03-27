using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleAppMVC_Identity.Data.Context;
using SampleAppMVC_Identity.Data.Models;
using System;

namespace SampleAppMVC_Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // تنظیمات اتصال به دیتابیس
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringDb")));


            // تنظیمات Identity
            builder.Services.AddIdentityCore<ApplicationUser>()
                     .AddEntityFrameworkStores<ApplicationDbContext>()
                     .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // این بخش قوانین مربوط به رمز عبور کاربران را مشخص می‌کند.
                // تنظیمات اعتبارسنجی رمز عبور
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;

                // تنظیمات قفل کردن حساب کاربری
                // گر کاربر چندین بار رمز عبور اشتباه وارد کند، حساب او برای 5 دقیقه قفل می‌شود.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                // اگر کاربر 5 بار رمز عبور اشتباه وارد کند، حساب او قفل می‌شود. 
                options.Lockout.MaxFailedAccessAttempts = 5;

                // تنظیمات کوکی‌ها و احراز هویت
                // کاربر نیازی به تأیید ایمیل خود ندارد تا بتواند وارد حساب کاربری شود.
                options.SignIn.RequireConfirmedEmail = false;
                //  کاربر نیازی به تأیید حساب کاربری خود ندارد (مثلاً از طریق لینک فعال‌سازی).
                options.SignIn.RequireConfirmedAccount = false;
            });

            builder.Services.AddScoped<UserManager<ApplicationUser>>();

            // افزودن Authentication و Authorization
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // قبل از UseAuthorization قرار دارد
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapRazorPages();

            app.Run();
        }
    }
}
