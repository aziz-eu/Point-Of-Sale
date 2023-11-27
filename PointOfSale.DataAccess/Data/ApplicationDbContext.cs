using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet <Category> Categories {  get; set; }
        public DbSet <Supplier> Suppliers { get; set; }
        public DbSet <UnitsOfMeasurement> UnitsOfMeasurement { get; set;}
        public DbSet <Product> Products { get; set; }

        public DbSet <ApplicationUser> ApplicationUsers { get; set; }

    }
}
