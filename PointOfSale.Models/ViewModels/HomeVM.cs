using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModels
{
    public class HomeVM
    {
        public int CountProduct { get; set; }
        public double CalculateInvoiceAmount { get; set; }
        public double CalculateDueInvoiceAmount { get; set; }
        public int CountInvoice {  get; set; }
        public int CountDueInvoice { get; set; }
        public int CountDeliveryNote {  get; set; }


    }
}
