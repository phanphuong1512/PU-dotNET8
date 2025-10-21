using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PU.Models.Entities
{
    [Table("products")]
    public class Product
    {
        [Column("product_id")] public int ProductId { get; set; }

        [Column("name")] public string Name { get; set; } = string.Empty;

        [Column("description")] public string? Description { get; set; }

        [Column("price")] public decimal Price { get; set; }

        [Column("stock")] public int Stock { get; set; }

        [Column("category_id")] public int? CategoryId { get; set; }

        [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Category? Category { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}