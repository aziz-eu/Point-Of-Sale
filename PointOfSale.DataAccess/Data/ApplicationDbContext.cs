using Microsoft.EntityFrameworkCore;
using PointOfSale.Models;

namespace PointOfSale.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet <Category> Categories {  get; set; }
        public DbSet <Supplier> Suppliers { get; set; }
        public DbSet <UnitsOfMeasurement> UnitsOfMeasurement { get; set;}

    }
}
