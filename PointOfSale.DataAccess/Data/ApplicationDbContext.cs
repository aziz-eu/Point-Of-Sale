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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
             .HasOne(e => e.Category)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
            .HasOne(e => e.Supplier)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
           .HasOne(e => e.UnitsOfMeasurement)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

           

            modelBuilder.Entity<Cart>()
           .HasOne(e => e.Product)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceHeader>()
                .HasOne(e=>e.ApplicationUser)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(e=>e.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(e=>e.InvoiceHeader)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet <Category> Categories {  get; set; }
        public DbSet <Supplier> Suppliers { get; set; }
        public DbSet <UnitsOfMeasurement> UnitsOfMeasurement { get; set;}
        public DbSet <Product> Products { get; set; }
        public DbSet <ApplicationUser> ApplicationUsers { get; set; }
        public DbSet <Cart> Carts { get; set; }
        public DbSet <VatRate> VatRates { get; set; }
        public DbSet <InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Company> Companys { get; set; }
       
    }
}
