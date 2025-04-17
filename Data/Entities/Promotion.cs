using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Entities
{
    public class Promotion
    {
        [Key]
        public Guid Id { get; set; }
        public string CodePromotion { get; set; }

        [Required(ErrorMessage = "Tên khuyến mãi không được để trống.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Tên khuyến mãi phải từ 3 đến 25 ký tự.")]
        public string NamePromotion { get; set; }

        [MaxLength(256, ErrorMessage = "Ghi chú không được vượt quá 256 ký tự.")]
        public string? Notes { get; set; }
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống.")]
        public DateTime EndDate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }     
        public string? ModifiedBy { get; set; }
        public virtual ICollection<PromotionDetail> PromotionDetail { get; set; } = new List<PromotionDetail>();

    }
}
