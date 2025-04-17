using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class CartDetail
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("CardId")]
        public Guid? CardId { get; set; }
        public Cart? Card { get; set; }
        [ForeignKey("ProductItemId")]
        public Guid ProductItemId { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}
