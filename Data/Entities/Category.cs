using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Tên danh mục phải từ 3 đến 30 ký tự.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
