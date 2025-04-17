using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string CodeRole { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        public DateTime? CreatedTime { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public virtual ICollection<User>? User { get; set; } = new List<User>();


    }
}
