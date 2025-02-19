using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDms.Models
{
    public class TreeDirectory
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string? icon { get; set; }
        public string? state { get; set; }
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        public string? li_attr { get; set; }
        public string? a_attr { get; set; }
    }
}
