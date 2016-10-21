using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAH.ViewModel
{
    public class UserView
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

       
        public string Address { get; set; }

       
        public string City { get; set; }

       
        public string State { get; set; }

       
        public string Zip { get; set; }


        public string Email { get; set; }

    
        public string Phone { get; set; }

    
        public string OtherPhone { get; set; }

        public string Company { get; set; }
        public string ContactName { get; set; }
        public bool ISDropShipAddress { get; set; }
        public string DropShipCompany { get; set; }
        public string DropShipAddress { get; set; }
        public string DropShipCity { get; set; }
        public string DropShipState { get; set; }
        public string DropShipZip { get; set; }
        public bool AutoConfirmation { get; set; }
        public bool DepositRequired { get; set; }
        public bool DepositConfirmation { get; set; }
        public string TermsCode { get; set; }
        public string ShipVia { get; set; }
        public string PricingPage { get; set; }
        public double PricingPercent { get; set; }
        public string Keywords { get; set; }
        public string Notes { get; set; }
        public bool CustomerInfoSent { get; set; }
        public bool Active { get; set; }
        public string CustomerCode { get; set; }

        public int PriceList { get; set; }
        public string VendorMFG { get; set; }
    }
}