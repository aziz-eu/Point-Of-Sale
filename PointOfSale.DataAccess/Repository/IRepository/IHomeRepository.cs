using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.DataAccess.Repository.IRepository
{
    public interface IHomeRepository
    {
        int CountProduct();

        double CalculateInvoiceAmount();
        double CalculateDueInvoiceAmount();
        int CountInvoice();
        int CountDueInvoice();
        int CountDeliveryNote();
    }
}
