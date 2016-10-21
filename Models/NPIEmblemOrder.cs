using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class NPIEmblemOrder
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Customer Code")]
        public int CustomerCode { get; set; }
        public string Terms { get; set; }
        [DisplayName("PO Number")]
        public string PONumber { get; set; }
        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("Request Date")]
        public DateTime? RequestDate { get; set; }
        [DisplayName("rush Order")]
        public bool RushOrder { get; set; }
        
        public double? RushOrderPercent { get; set; }
        [DisplayName("Is this an Event?")]
        public bool ISEvent { get;set; }
        [DisplayName("Ship Via")]
        public int ShipVia{get;set;}
        
        public int Sample{get;set;}
        
        public int Quantity{get;set;}
        [DisplayName("Emblem Name")]
        public string EmblemName{get;set;}
        [DisplayName("First order?")]
        public bool FirstOrder{get;set;}
        [DisplayName("Reorder Br #")]
        public string ReorderBrNumber{get;set;}
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Size{ get; set; }

        public int Shape{ get; set; }

        [DisplayName("Total Colors")]
        public int TotalColors{ get; set; }

        [DisplayName("1 Color Break")]
        public string ColorBreak1 { get; set; }
        [DisplayName("2 Color Break")]
        public string ColorBreak2 { get; set; }
        [DisplayName("3 Color Break")]
        public string ColorBreak3 { get; set; }
        [DisplayName("4 Color Break")]
        public string ColorBreak4 { get; set; }
        [DisplayName("5 Color Break")]
        public string ColorBreak5 { get; set; }
        [DisplayName("6 Color Break")]
        public string ColorBreak6 { get; set; }
        [DisplayName("7 Color Break")]
        public string ColorBreak7 { get; set; }
        [DisplayName("8 Color Break")]
        public string ColorBreak8 { get; set; }

        [DisplayName("9 Color Break")]
        public string ColorBreak9 { get; set; }
        [DisplayName("10 Color Break")]
        public string ColorBreak10 { get; set; }
        [DisplayName("11 Color Break")]
        public string ColorBreak11 { get; set; }
        [DisplayName("12 Color Break")]
        public string ColorBreak12 { get; set; }

        [DisplayName("Duplicate Sample/artwork exactly")]
        public bool DuplicateSample { get; set; }
        [DisplayName("As sample/artwork with changes")]
        public bool AsSample { get; set; }
        [DisplayName("Tung Li")]
        public bool TungLi { get; set; }
        [DisplayName("Reoder exactly as before")]
        public bool ReorderAsBefore { get; set; }
        [DisplayName("Reorder with change")]
        public bool ReorderWithChange { get; set; }
        [DisplayName("Fine Thread")]
        public bool FineThread { get; set; }

        
        public string Company { get;set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [DisplayName("Base price")]
        public double? BasePrice { get; set; }
        [DisplayName("Price each")]
        public double? PriceEach { get; set; }

        [DisplayName("Ordered By")]
        public int OrderedBy { get; set; }

        [DisplayName("% of Emblem")]
        public int PercentOfEmblem { get; set; }

        
        public string Background { get;set; }

        [DisplayName("Type/Price%")]
        public int TypePricePercent { get;set; }

        
        public double TypePricePercentValue { get; set; }

        
        public string Border { get; set; }

        [DisplayName("Type/Price")]
        public int TypePrice { get; set; }

        
        public double? TypePriceValue{get;set;}

        public int Backing { get;set; }

        public double? BackingPrice { get; set; }

        [DisplayName("Special Thread/%")]
        public int SpecialThread { get; set; }

        
        public double? SpecialThreadValue { get; set; }

        [DisplayName("Special Instructions")]
        [DataType(DataType.MultilineText)]
        public string SpecialInstructions { get; set; }
    //    [DisplayName("Price List")]
    //    public int PriceListID { get; set; }
    }
}