using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public bool? IsActive { get; set; } 
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public virtual ICollection<CartDetail>? CartDetail { get; set; } = new List<CartDetail>();
    }
}
