using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class AddressUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Ward {  get; set; }
        public string District { get; set; }
        public string CityTown { get; set; }
        public bool Status { get; set; }
        public string? AddressDetail { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
