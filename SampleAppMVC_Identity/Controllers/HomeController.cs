using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleAppMVC_Identity.Data.Models;
using SampleAppMVC_Identity.Models;

namespace SampleAppMVC_Identity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // یک سرویس برای مدیریت کاربران (مثل ایجاد، حذف، ویرایش کاربران، تغییر رمز عبور و غیره) فراهم کند.
        private readonly UserManager<ApplicationUser> _userManager;
        // یک سرویس برای مدیریت لاگین و لاگ‌آوت کاربران (مثل ورود به سیستم، خروج از سیستم، بررسی وضعیت لاگین و غیره) فراهم کند.
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {

            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
