using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class Customer
    {
        public int Customer_ID { get; set; }
        public string Customer_Code { get; set; }
        public string Company { get; set; }
        public string Contact_Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax_Other_Phone { get; set; }
        public bool Use_Drop_Ship_Address_ { get; set; }
        public string Drop_Ship_Company { get; set; }
        public string Drop_Ship_Address { get; set; }
        public string Drop_Ship_City { get; set; }
        public string Drop_Ship_State { get; set; }
        public string Drop_Ship_Zip { get; set; }
        public bool Auto_Confirmation_ { get; set; }
        public bool Deposit_Required_ { get; set; }
        public bool Deposit_Confirmation_ { get; set; }
        public string Terms_Code { get; set; }
        public string Ship_Via { get; set; }
        public string Pricing_Page { get; set; }
        public Nullable<float> Pricing_Percent { get; set; }
        public string Keywords { get; set; }
        public string Notes { get; set; }
        public bool CustomerInfoSent { get; set; }
    }
}