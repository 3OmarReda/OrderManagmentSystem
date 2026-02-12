using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<User> Users { get; set; } 

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Invoice> Invoices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            optionsBuilder.LogTo(log => Debug.WriteLine(log), LogLevel.Information).
                EnableSensitiveDataLogging(true); // Enable sensitive data logging for debugging purposes

            base.OnConfiguring(optionsBuilder);
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // Apply all configurations from the current assembly
            GuidDefaultValue(modelBuilder); // Set default value for Guid Id properties
            base.OnModelCreating(modelBuilder);
        }

        private void GuidDefaultValue(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var idProperty = entityType.FindProperty("Id");
                if (idProperty != null && idProperty.ClrType == typeof(Guid))
                {
                    idProperty.SetDefaultValueSql("NEWSEQUENTIALID()");
                }
            }
        }
       
      
    }
}
