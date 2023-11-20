﻿using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
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
           
        
        }
        public ICategoryRepository Category { get; private set; }
        public ISupplierRepository Supplier { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
