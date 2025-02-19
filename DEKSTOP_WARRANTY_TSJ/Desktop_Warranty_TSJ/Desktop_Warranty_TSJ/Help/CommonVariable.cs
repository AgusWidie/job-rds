using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Desktop_Warranty_TSJ.Help
{
    public static class CommonVariable
    {
        public static string baseUrl = "http://192.168.0.13:8081/api";
        //public static string baseUrl = "https://localhost:7214/api";
        public static string Token = "";
        public static string CreatedBy = "";
        public static string SourcePrinter = "TSJ";
    }
}
