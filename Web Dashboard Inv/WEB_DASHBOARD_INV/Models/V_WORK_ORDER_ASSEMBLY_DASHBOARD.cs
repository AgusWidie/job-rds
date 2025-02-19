using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_ERP_TSJ.Models
{
    public class V_WORK_ORDER_ASSEMBLY_DASHBOARD
    {
        public int id { get; set; }
        public string operation_id { get; set; }
        public string company_id { get; set; }
        public Nullable<bool> priority_status { get; set; }
        public string work_order_name { get; set; }
        public Nullable<System.DateTime> operation_date { get; set; }
        public Nullable<System.DateTime> finished_operation_date { get; set; }
        public string so_id { get; set; }
        public string wh_id { get; set; }
        public string product_group_id { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string dimention { get; set; }
        public string variant_id { get; set; }
        public string variant_name { get; set; }
        public int @long { get; set; }
        public int wide { get; set; }
        public int tall { get; set; }
        public Nullable<int> qty_order { get; set; }
        public Nullable<decimal> m3_order { get; set; }
        public Nullable<int> qty_spp { get; set; }
        public Nullable<decimal> m3_spp { get; set; }
        public Nullable<int> qty_requested { get; set; }
        public Nullable<decimal> m3_requested { get; set; }
        public int qty_done { get; set; }
        public string note { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    }
}