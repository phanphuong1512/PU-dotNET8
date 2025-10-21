using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PU.Models.Entities
{
    [Table("orders")]
    public class Order
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        public User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}