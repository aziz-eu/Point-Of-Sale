
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
    public class InvoiceHeader
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double SubTotal { get; set; }
        public double Vat {  get; set; }
        public double Total { get; set; }

        [Required]
        [Display(Name="Paid Amount")]
        public double PaidAmount { get; set; }
        public double UnpaidAmount { get; set; }
        public string PaymentSataus { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Phone")]
        public string? PhoneNumbar { get; set; } 
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? ApplicationUserId {  get; set; }
        [ForeignKey (nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [NotMapped]
        public double UpdateDue {  get; set; }

    }
}
