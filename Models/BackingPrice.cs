using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    [Table("BackingPrice")]
    public class BackingPrice
    {
        public int ID { get; set; }
        public int BackingType { get; set; }
        public double Size { get; set; }
        public double Cost { get;set; }
    }
}