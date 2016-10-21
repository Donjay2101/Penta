using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


        [Compare("Password",ErrorMessage="password and confirm password do not match.")]
        [NotMapped]
        [Required]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }


        public string Address { get; set; }


        public string City { get; set; }


        public string State { get; set; }


        public string Zip { get; set; }

        [Required]
        public string Email { get; set; }


        public string Phone { get; set; }

        [DisplayName("Other Phone")]
        public string OtherPhone { get; set; }

        public string Company { get; set; }
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [DisplayName("IS Drop Ship Address")]
        public bool ISDropShipAddress { get; set; }
        [DisplayName("Drop Ship Company")]
        public string DropShipCompany { get; set; }
        [DisplayName("Drop Ship Address")]
        public string DropShipAddress { get; set; }
        [DisplayName("Drop Ship City")]
        public string DropShipCity { get; set; }
        [DisplayName("Drop Ship State")]
        public string DropShipState { get; set; }
        [DisplayName("Drop Ship Zip")]
        public string DropShipZip { get; set; }
        [DisplayName("Auto Confirmation")]
        public bool AutoConfirmation { get; set; }
        [DisplayName("Deposit Required")]
        public bool DepositRequired { get; set; }
        [DisplayName("Deposit Confirmation")]
        public bool DepositConfirmation { get; set; }
        [DisplayName("Terms Code")]
        public string TermsCode { get; set; }
        [DisplayName("Ship Via")]
        public string ShipVia { get; set; }
        [DisplayName("Pricing Page")]
        public string PricingPage { get; set; }
        [DisplayName("Pricing Percent")]
        public double PricingPercent { get; set; }

        public string Keywords { get; set; }

        public string Notes { get; set; }
        [DisplayName("Customer InfoSent")]
        public bool CustomerInfoSent { get; set; }

        public bool Active { get; set; }

        //public int? PriceList { get; set; }
        //public string VendorMFG { get; set; }
    }
}