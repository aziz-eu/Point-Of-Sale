using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class VatRate
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "VAT (%)")]
        [Range(0,100, ErrorMessage ="VAT Must be 0 to 100 %")]
        public double Vat { get; set; }
    }
}
