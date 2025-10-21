using System.ComponentModel.DataAnnotations.Schema;

namespace PU.Models.Entities
{
    [Table("order_details")]
    public class OrderDetail
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}