using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestWebApplication.Models;

namespace TestWebApplication.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Sprint>().ToTable("Sprint");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Models.Task>().ToTable("Task");
            modelBuilder.Entity<PasswordResetCode>().ToTable("PasswordResetCode");
        }
    }
}
