using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string? CodeOrder { get; set; }
        [Precision(10, 0)]
        public decimal? TotalPrice { get; set; }
        public string? OrderType { get; set; }
        public string? PhoneMumber { get; set; }
        public string? Email { get; set; }
        public string? OrderStatus { get; set; }
        public string? Address { get; set; }
        [Precision(10, 0)]
        public decimal? ShippingFee { get; set; }

        [Precision(10, 0)]
        public decimal? ReduceTotalPrice { get; set; }

        public string? Notes { get; set; }
        public bool Status { get; set; }
        public bool MethodPayment { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        [ForeignKey("VoucherId")]
        public Guid? VoucherId { get; set; }
        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public Voucher? Voucher { get; set; }
        public User? User { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
