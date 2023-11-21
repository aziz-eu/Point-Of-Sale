using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PointOfSale.Models
{
    public class Category
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
