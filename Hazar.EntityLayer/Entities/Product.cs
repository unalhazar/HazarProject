using System.ComponentModel.DataAnnotations;

namespace Hazar.EntityLayer.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
