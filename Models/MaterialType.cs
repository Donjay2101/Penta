using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    [Table("MaterialType")]
    public class MaterialType
    {
        public int ID { get;set; }
        public string Name { get;set; }
        public double Value { get; set; }

    }
}