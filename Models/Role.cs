using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class Role
    {

        public int ID { get;set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}