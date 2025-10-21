using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PU.Models.Entities
{
    [Table("categories")]
    public class Category
    {
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}