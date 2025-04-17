using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
        public bool? Gender { get; set; }
        public bool Status { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? CreateBy { get; set; }
        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public virtual ICollection<Cart>? Cart { get; set; } = new List<Cart>(); 
        public virtual ICollection<Order>?  Order { get; set; } = new List<Order>();
        public virtual ICollection<VoucherUser>? Voucher_User { get; set; } = new List<VoucherUser>();
        public virtual ICollection<AddressUser> AddressUser { get; set; } = new List<AddressUser>();

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
