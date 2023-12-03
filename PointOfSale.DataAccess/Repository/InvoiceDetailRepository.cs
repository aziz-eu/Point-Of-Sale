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
    public class InvoiceDetailRepository : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        public ApplicationDbContext _db;

        public InvoiceDetailRepository(ApplicationDbContext db) : base (db)
        {
            
        }
        public void Update(InvoiceDetail invoiceDetail)
        {
            _db.InvoiceDetails.Update(invoiceDetail);
        }
    }
}
