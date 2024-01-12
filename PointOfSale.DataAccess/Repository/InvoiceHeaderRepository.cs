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
    public class InvoiceHeaderRepository : Repository<InvoiceHeader>, IInvoiceHeaderRepository
    {
        public ApplicationDbContext _db;
        public InvoiceHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public double CalculateDue(int? id)
        {
           double totalDue = _db.InvoiceHeaders.Where(u => u.RegularCustomerId == id).Sum(c => c.UnpaidAmount);
            return totalDue;
        }

        public void Update(InvoiceHeader invoiceHeader)
        {
            _db.InvoiceHeaders.Update(invoiceHeader);
        }

        public void UpdatePaymentStatus(int id, string? paymentStatus = null)
        {
            var obj = _db.InvoiceHeaders.FirstOrDefault(x => x.Id == id);
            if (paymentStatus!=null)
            {
                obj.PaymentSataus = paymentStatus;
            }
        }

        public double UpdateUnPaidAmount(int id, double amount)
        {
            var obj = _db.InvoiceHeaders.FirstOrDefault(x => x.Id == id);
            return 0;

        }


    }
}
