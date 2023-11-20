using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Company { get; set; }
        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
