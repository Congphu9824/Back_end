using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Entities
{
    [Table("ProductItem")]
    public class ProductItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? CodeProductItem { get; set; }
        [Precision(10, 0)]
        public decimal Price { get; set; }
        [Precision(10, 0)]
        public decimal? DiscountPrice { get; set; }
        public int Quantity { get; set; }   
        public string? Notes { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public string ImgDetail { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("ProductId")]
        public Guid? ProductId { get; set; }
        [ForeignKey("ColorId")]
        public Guid? ColorId { get; set; }
        [ForeignKey("SizeId")]
        public Guid? SizeId { get; set; }
        public Product? Product { get; set; }
        public Color? Color { get; set; }
        public Size? Size { get; set; }

        [JsonIgnore]

        public virtual ICollection<OrderDetail>? OrderDetail { get; set; } = new List<OrderDetail>();
        public virtual ICollection<CartDetail>? CartDetail { get; set; } = new List<CartDetail>();
        public virtual ICollection<PromotionDetail>? PromotionDetail { get; set; } = new List<PromotionDetail>();

    }
}
