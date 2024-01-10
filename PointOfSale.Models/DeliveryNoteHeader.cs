
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
    public class DeliveryNoteHeader
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Phone")]
        public string? PhoneNumbar { get; set; } 
        public string? Email { get; set; }
        public string? Address { get; set; }
      
        public string? CustTrn { get; set; }


    }
}
