using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class DeliveryNoteDetail
    {
        public int Id { get; set; }

        public int DeliveryNoteId { get; set; }

        [ForeignKey(nameof(DeliveryNoteId))]
        [ValidateNever]
        public DeliveryNoteHeader DeliveryNoteHeader { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public double Count {  get; set; }

    }
}
