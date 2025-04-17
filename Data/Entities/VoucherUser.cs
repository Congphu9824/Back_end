using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class VoucherUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid VoucherId { get; set; }
        public Voucher Voucher { get; set; }
        public Guid UserId { get; set; }
        public  User User { get; set; }  
    }
}
