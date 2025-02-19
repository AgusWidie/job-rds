using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class ResponseMessage
    {
        public int code { get; set; }
        public int status { get; set; }
        public bool IsSucceed { get; set; }
        public string message { get; set; }
        public List<ResponseErrorMessage> list_msg { get; set; }
    }

    public class ResponseErrorMessage
    {
        public string msg { get; set; }
    }
}