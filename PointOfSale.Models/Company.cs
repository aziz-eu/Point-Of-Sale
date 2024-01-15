using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Name in Aribic")]
        public string AribicName { get; set; }
        [Required]
        public string TRN {  get; set; }
       
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number 1")]
        public string PhoneNumber1 { get; set; }
        [Required]
        [Display(Name = "Phone Number 2")]
        public string PhoneNumber2 { get; set;}
        [Required]
        [Display(Name = "Phone Number 1 in Aribic")]
        public string AribicPhoneNumber1 { get; set; }
        [Required]
        [Display(Name = "Phone Number 2 in Aribic")]
        public string AribicPhoneNumber2 { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address {  get; set; }
        [Required]
        [Display(Name = "Address in Aribic")]
        public string AribicAddress { get; set; }

        [Required]
        [Display(Name = "P.O Box No")]
        public string PostOfficeNo { get; set; }

        [Required]
        [Display(Name= "Aribic P.O Box No")]
        public string AribicPostOfficeNo { get; set; }




    }
}
