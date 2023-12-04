using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Range(1,100000)]
        public double Price { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Value Mustbe Getter Then 1")]
        public int Count { get; set; }

        [Required]
        public int ProdouctId { get; set; }
        [ForeignKey(nameof(ProdouctId))]
        [ValidateNever]
        public Product Product { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
