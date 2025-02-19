using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ.Help
{
    public class GlobalResponse
    {
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }

    public class GlobalErrorResponse
    {
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
    }
    public class GlobalObjectResponse
    {
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class GlobalObjectListResponse
    {
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public List<Object> Data { get; set; }
    }

    public class GlobalObjectResponse<T>
    {
        public int Code { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
