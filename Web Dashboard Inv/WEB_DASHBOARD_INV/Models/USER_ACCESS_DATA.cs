using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_ERP_TSJ.Models
{
    public class USER_ACCESS_DATA
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string wh_id { get; set; }
        public string product_group_id { get; set; }
        public string product_category_id { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
        public string created_by { get; set; }
    }
}