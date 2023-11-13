using System.ComponentModel.DataAnnotations;

namespace PointOfSaleWeb.Models
{
    public class Category
    {
      
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
