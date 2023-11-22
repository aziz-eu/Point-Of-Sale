using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? BarCode { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]            
        public int UnitPrice { get; set; }
        public int? SellPrice { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category Category { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [ValidateNever]
        public Supplier Supplier { get; set; }

        public int UnitsOfMeasurementId { get; set; }

        [ForeignKey(nameof(UnitsOfMeasurementId))]
        [ValidateNever]
        public UnitsOfMeasurement UnitsOfMeasurement { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;

    }
}
