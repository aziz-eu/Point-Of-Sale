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
        public string AribicName { get; set; }
        [Required]
        public string TRN {  get; set; }
        [Required]
        public string ClstTRN { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber1 { get; set; }
        [Required]
        public string PhoneNumber2 { get; set;}
        [Required]
        public string AribicPhoneNumber1 { get; set; }
        [Required]
        public string AribicPhoneNumber2 { get; set; }
        [Required]
        public string Address {  get; set; }
        [Required]
        public string AribicAddress { get; set; }




    }
}
