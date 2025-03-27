using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleAppMVC_Identity.Data.Models;
using System;

namespace SampleAppMVC_Identity.Data.Context
{
    // اضافه کردن فیلدهای اضافه با کلاس AppicationUser نوسط IdentityDbContext به مدل user
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }

    }
}
