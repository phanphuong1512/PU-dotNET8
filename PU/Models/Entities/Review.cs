    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace PU.Models.Entities
    {
        [Table("reviews")]
        public class Review
        {
            [Column("review_id")]
            public int ReviewId { get; set; }

            [Column("user_id")]
            public int UserId { get; set; }

            [Column("product_id")]
            public int ProductId { get; set; }

            [Column("rate")]
            public int Rate { get; set; }

            [Column("comment")]
            public string? Comment { get; set; }

            [Column("created_at")]
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public User? User { get; set; }
            public Product? Product { get; set; }
        }
    }