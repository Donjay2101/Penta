using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAH.ViewModel
{
    public class PriceListViewModel
    {
        public int ID { get; set; }
        public double Size { get; set; }
        public string Emblem { get; set; }
        public double First { get; set; }
        public double Second { get; set; }
        public double Third { get; set; }
        public double Fourth { get; set; }
        public double Fifth { get; set; }
        public double Sixth { get; set; }
        public double Seventh { get; set; }
        public double Eighth { get; set; }
        public string PriceListName { get; set; }

        public double HeatSeal { get; set; }
        public double PatternFee { get; set; }
        public int PriceListNameID { get; set; }

    }
}