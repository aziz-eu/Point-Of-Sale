﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public InvoiceHeader InvoiceHeader { get; set; }
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
        public DeliveryNoteHeader DeliveryNoteHeader { get; set; }
        public IEnumerable <DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
        public IEnumerable<UnitsOfMeasurement> UnitsOfMeasurement { get; set; }
        public VatRate VatRate { get; set; }
        public IEnumerable<Cart> ListCart { get; set; }
        public Company Company { get; set; }
        
        public string AmountInWord { get; set; }
        public double TotalDueAmount { get; set; }
        
        public double VatPercentage { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CustomersList  { get; set; }


    }
}
