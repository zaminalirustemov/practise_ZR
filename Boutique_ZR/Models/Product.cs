using System.ComponentModel.DataAnnotations.Schema;

namespace Boutique_ZR.Models
{
    public class Product:Base
    {
        public int CategoryId { get; set; }
        public int TagId { get; set; }

        public string? ImageUrl { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        public string Description { get; set; }
        public int SKU { get; set; }

        public Category? Category { get; set; }
        public Tag? Tag { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
