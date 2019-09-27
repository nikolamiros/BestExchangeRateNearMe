using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<ExchangeRate>()
                .HasKey(c => new { c.ExchangeOfficerId, c.Currency });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        
        public DbSet<ExchangeOfficer> ExchangeOfficers { get; set; }

        public DbSet<ExchangeCustomer> ExchangeCustomers { get; set; }
    }
}
