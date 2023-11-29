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
        public Product Product { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
