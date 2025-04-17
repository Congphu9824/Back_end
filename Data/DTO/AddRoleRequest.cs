using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.DTO
{
    public class AddRoleRequest
    {
        public string Name { get; set; }
        public string CodeRole { get; set; }
        public string CreateBy { get; set; }
    }
}
