using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
	public class PromotionDetail
	{
		[Key]
		public Guid Id { get; set; }

		[Precision(10, 0)]
        [Required(ErrorMessage = "DiscountValue không được để trống.")]

        public decimal DiscountValue { get; set; }

        [Required(ErrorMessage = "DiscountValue không được để trống.")]
        public string DiscountType { get; set; }


		[ForeignKey("ProductItemId")]
		public Guid ProductItemId { get; set; }
		public ProductItem ProductItem { get; set; }
		[ForeignKey("PromotionId")]
		public Guid PromotionId { get; set; }
		public Promotion Promotion { get; set; }



	}
}
