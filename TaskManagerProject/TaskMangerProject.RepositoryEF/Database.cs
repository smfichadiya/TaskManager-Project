using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProject.Domain.RepositoryEF
{
    public class Database : DbContext
    {
        public Database() : base("name=DefaultConnection")
        { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<DashboardWidget> DashboardWidgets { get; set; }
    }
}
