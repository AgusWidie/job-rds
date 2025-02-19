using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class LoginViewModel
    {
        public string company { get; set; }
        public string platform { get; set; }
        public string user_id { get; set; }
        public string web_id { get; set; }
        public string telepon { get; set; }
        public string password { get; set; }
    }
}