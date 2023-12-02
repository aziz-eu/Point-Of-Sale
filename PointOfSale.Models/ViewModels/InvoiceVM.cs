using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModels
{
    public class InvoiceVM
    {
        public Cart Cart { get; set; }
        public IEnumerable<Cart> ListCart { get; set; }

        public double CartTotal { get; set; }
        public double VatRate { get; set; }
        public double VatTotal {  get; set; }
        public double Total { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
      

    }
}
