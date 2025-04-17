using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATA.Entities
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        [Precision(10, 0)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; } 
        public DateTime? ModifiedTime { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
