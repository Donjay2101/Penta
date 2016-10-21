using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class PriceList
    {
        public int ID { get; set; }
        public double Size { get; set; }
        public int Emblem { get; set; }
        [DisplayName("50")]
        public double First { get; set; }
        [DisplayName("100")]
        public double Second { get; set; }
        [DisplayName("200")]
        public double Third { get; set; }
        [DisplayName("300")]
        public double Fourth { get; set; }
        [DisplayName("500")]
        public double Fifth { get; set; }
        [DisplayName("1000")]
        public double Sixth { get; set; }
        [DisplayName("2000")]
        public double Seventh { get; set; }
        [DisplayName("3000")]
        public double Eighth { get; set; }
        [DisplayName("5000")]
        public double Ninth { get; set; }
        [DisplayName("10000")]
        public double Tenth { get; set; }
        [DisplayName("Price List")]
        public double HeatSeal { get; set; }
        public double PatternFee { get; set; }

        public int PriceListNameID { get; set; }

    }
}