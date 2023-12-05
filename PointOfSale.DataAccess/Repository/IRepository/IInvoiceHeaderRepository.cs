using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface IInvoiceHeaderRepository : IRepository<InvoiceHeader>
    {
        void Update (InvoiceHeader invoiceHeader);

        void UpdatePaymentStatus(int id, string? paymentStatus = null);

        double UpdateUnPaidAmount(int id ,double amount);
    }
}
