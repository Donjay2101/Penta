using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    [Table("BorderType")]
    public class BorderType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
    }
}