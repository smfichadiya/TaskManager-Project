using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManagerProject.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskManagerProject.RepositoryEF;

namespace TaskManagerProject.Domain.RepositoryEF
{
    public class MyDatabase : IdentityDbContext<AppUser>
    {
        public MyDatabase() : base("name=DefaultConnection")
        {
           
        }

        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<MyUser> MyUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
    }
}
