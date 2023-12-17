using System.ComponentModel.DataAnnotations;

namespace Hazar.EntityLayer.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
