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
            .OnDelete(DeleteBehavior.SetNull);

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

            modelBuilder.Entity<VatRate>().HasData(
                new VatRate {
                    Id = 1,
                    Vat = 5
                });
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 1,
                Name = "Company Name",
                AribicName = "Company Name",
                TRN = "123456789",
                ClstTRN = "123456789",
                Email = "someting@mail.com",
                PhoneNumber1 = "1234567890",
                PhoneNumber2 = "1234567890",
                AribicPhoneNumber1 = "1234567890",
                AribicPhoneNumber2 = "1234567890",
                Address = "something, UAE",
                AribicAddress = "someting",
                PostOfficeNo = "1234567890",
                AribicPostOfficeNo = "1234567890",
            });

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
        public DbSet<DeliveryNote> deliveryNotes { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet <Customer> Customers { get; set; }
       

    }
}
