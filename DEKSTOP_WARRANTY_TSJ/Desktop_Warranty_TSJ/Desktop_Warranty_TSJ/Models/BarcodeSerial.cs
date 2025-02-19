using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ.Models
{
    public class BarcodeSerial
    {
        public long Id { get; set; }
        public string BarcodeSerialId { get; set; }
        public string SerialCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
