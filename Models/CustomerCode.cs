using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class CustomerCode
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int UserID { get; set; }
        public int PriceList { get; set; }
        public string VendorMFG { get; set; }
    }
}