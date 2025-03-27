using Microsoft.AspNetCore.Identity;

namespace SampleAppMVC_Identity.Data.Models
{
    // ایجاد فیلد سفارشی در مدل ApplicationUser به واسطه IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // می‌توانید فیلدهای اضافی برای کاربر اضافه کنید
        public string FullName { get; set; }
    }
}
