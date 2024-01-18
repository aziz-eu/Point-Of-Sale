using PointOfSale.Data;
using PointOfSale.DataAccess.Repository.IRepository;
using PointOfSale.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)  
        {
            _db = db;
        }

        public double CalculateDueInvoiceAmount()
        {
            double totalDueInvoiceAmount = _db.InvoiceHeaders.Sum(c => c.UnpaidAmount);
            return totalDueInvoiceAmount;
        }

        public double CalculateInvoiceAmount()
        {
            double totalInvoiceAmount = _db.InvoiceHeaders.Sum(c => c.Total);
            return totalInvoiceAmount;
        }

        public int CountDeliveryNote()
        {
            int deliveryNoteCount = _db.DeliveryNoteHeaders.Select(u => u.Id).Count();
            return deliveryNoteCount;

        }

        public int CountDueInvoice()
        {
            int dueInvoiceCount = _db.InvoiceHeaders.Where(u=>u.UnpaidAmount!=0).Select(u =>u.Id).Count();
            return dueInvoiceCount;
        }

        public int CountInvoice()
        {
            int countInvoice = _db.InvoiceHeaders.Select(u=>u.Id).Count();
            return countInvoice;
        }

        public int CountProduct()
        {
            int countProduct = _db.Products.Select(u=>u.Id).Count();
            return countProduct;

        }
    }
}
