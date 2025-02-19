using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEB_DASHBOARD_INV.Models;

namespace WEB_DASHBOARD_INV.ViewModels
{
    public class MenuViewModel
    {
        public List<V_MENU_PARENT_INV> ListMenuParent { get; set; }
        public List<V_MENU_INV> ListMenu { get; set; }
    }
}