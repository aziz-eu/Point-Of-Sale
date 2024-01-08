﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email {  get; set; }
        public string? Address { get; set; }
        public string? ClstTrn { get; set; }
    }
}
