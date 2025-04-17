using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Entities
{
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        public string CodeVoucher {  get; set; }

        [Required(ErrorMessage = "Tên Voucher không được để trống.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Tên Voucher phải từ 3 đến 25 ký tự.")]
        public string NameVoucher { get; set; }

        public int Quantity {  get; set; }

        [Precision(10, 0)]
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; }
        [Precision(18, 0)]
        public decimal MinimumOrderValue { get; set; }
        [Precision(18, 0)]
        public decimal? MaximumOrderValue { get; set; }
        public string? Notes {  get; set; }
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống.")]
        public DateTime EndDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public virtual ICollection<VoucherUser>? Voucher_User { get; set; } = new List<VoucherUser>();
        public virtual ICollection<Order>? Order { get; set; } = new List<Order>();

    }
}
