using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_DASHBOARD_INV.Models
{
    public class V_MENU_INV
    {
        public int id { get; set; }
        public string menu_name_parent { get; set; }
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public string initial_name { get; set; }
        public string menu_controller { get; set; }
        public string menu_action { get; set; }
        public Nullable<int> sort { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
    }
}