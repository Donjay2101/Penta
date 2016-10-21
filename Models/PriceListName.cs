namespace DAH
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceListName")]
    public partial class PriceListName
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
    }
}
