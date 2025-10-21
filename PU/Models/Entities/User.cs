using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PU.Models.Entities
{
    [Table("users")]
    public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}