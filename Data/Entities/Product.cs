using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DATA.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
		public DateTime? CreatedTime { get; set; }
		public string? CreateBy { get; set; }   
		public DateTime? ModifiedTime { get; set; } 
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        [ForeignKey("MaterialId")]
        public Guid MaterialId { get; set; }
        [ForeignKey("OriginId")]
        public Guid OriginId { get; set; }
        public Material? Material { get; set; }
        public Category? Category { get; set; }
        public Origin? Origin { get; set; }
        public virtual ICollection<ProductItem> ProductItem { get; set; } = new List<ProductItem>();
    }

}
