using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ.Models
{
    public class LoginRequest
    {
        public string Company { get; set; }
        public string Platform { get; set; }
        public string UserId { get; set; }
        public string WebId { get; set; }
        public string Telepon { get; set; }
        public string Password { get; set; }
    }
}
