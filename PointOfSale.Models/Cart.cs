using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProdouctId { get; set; }
        [ForeignKey(nameof(ProdouctId))]
        [ValidateNever]
        public Product Product { get; set; }
        public double Price { get; set; }
        public int count { get; set; }
        public int ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
