using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository
{
    public class VatRepository : Repository<VatRate>, IVatRateRepository
    {
       private ApplicationDbContext _db;
        public VatRepository(ApplicationDbContext db) : base(db) 
        {
            
        }
        public void Update(VatRate vatRate)
        {
            _db.VatRates.Update(vatRate);
        }
    }
}
