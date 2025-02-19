using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ.Models
{
    public class TemplatePrint
    {
        public long Id { get; set; }
        public string TemplateName { get; set; }
        public string Base64Logo { get; set; }
        public string Source { get; set; }
        public bool? Active { get; set; }
        public bool? PrintDefault { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
