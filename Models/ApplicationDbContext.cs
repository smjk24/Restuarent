using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarent.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Menu> Menus { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 1,
                    UserName = "admin",
                    Password="admin"
                });
            modelBuilder.Entity<Menu>()
                .HasOne(s => s.CLogin)
                .WithMany(s => s.CreatedMenu)
                .HasForeignKey(s => s.CLoginId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Menu>()
                .HasOne(s => s.MLogin)
                .WithMany(s => s.ModifiedMenu)
                .HasForeignKey(s => s.MLoginId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
