using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models.ViewModels
{
    public class DeliveryNoteVM
    {
 
     
        public DeliveryNoteHeader DeliveryNoteHeader { get; set; }
        public IEnumerable <DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public IEnumerable<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
        //public IEnumerable<Cart> ListCart { get; set; }
        public Company Company { get; set; }


        //[ValidateNever]
        //public IEnumerable<SelectListItem> ProductList { get; set; }
      

    }
}
