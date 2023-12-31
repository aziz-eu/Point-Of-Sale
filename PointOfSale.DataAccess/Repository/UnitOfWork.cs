﻿using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) {

            _db = db;
            Category = new CategoryRepository (_db);
            Supplier = new SupplierRepository (_db);
            UnitOfMeasurment = new UnitsOfMeasurementRepository(_db);
            Product = new ProductRepository (_db);
            Cart = new CartRepository (_db);
            ApplicationUser = new ApplicationUserRepository (_db);
            VatRate = new VatRepository (_db);
            InvoiceHeader = new InvoiceHeaderRepository(_db);
            InvoiceDetail = new InvoiceDetailRepository (_db);
            Company = new CompanyRepository (_db);
            Customer = new CustomerRepository (_db);


        }
        public ICategoryRepository Category { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IUnitsOfMeasurmentRepository UnitOfMeasurment { get; private set; }
        public IProductRepository Product {  get; private set; }

        public ICartRepository Cart {  get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IVatRateRepository VatRate { get; private set; }

        public IInvoiceDetailRepository InvoiceDetail {  get; private set; }

        public IInvoiceHeaderRepository InvoiceHeader { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
