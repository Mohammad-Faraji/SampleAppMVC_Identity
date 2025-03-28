using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleAppMVC_Identity.Data.Models;
using SampleAppMVC_Identity.Data.Models.ViewModel;
using SampleAppMVC_Identity.Models;

namespace SampleAppMVC_Identity.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        // یک سرویس برای مدیریت کاربران (مثل ایجاد، حذف، ویرایش کاربران، تغییر رمز عبور و غیره) فراهم کند.
        private readonly UserManager<ApplicationUser> _userManager;
        // یک سرویس برای مدیریت لاگین و لاگ‌آوت کاربران (مثل ورود به سیستم، خروج از سیستم، بررسی وضعیت لاگین و غیره) فراهم کند.
        private readonly SignInManager<ApplicationUser> _signInManager;

     

        public HomeController( UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {                 
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // ایجاد کاربرجدید
            var user = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                FullName = viewModel.FullName
                
            };
            // این خط کد برای ایجاد یک کاربر جدید استفاده می‌شود.
            var resultAddUser = await _userManager.CreateAsync(user,viewModel.Password);
            if (resultAddUser.Succeeded) 
            {
                var additionalClaims = new List<Claim>()
                {
                    new Claim("Name" , user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("UserName" , user.UserName)
                };
                await _signInManager.SignInWithClaimsAsync(user, true, additionalClaims);
                return RedirectToAction("Index");
            }


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
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
