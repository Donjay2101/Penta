using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class DAHDbContext:DbContext
    {
        public DAHDbContext():base("DefaultConnection")
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NPIEmblemOrder> Orders { get; set; }
        public DbSet<CustomerCode> CustomerCodes { get; set; }
        public DbSet<PriceList> PriceLists { get;set;}
        public DbSet<Emblem> Emblems { get; set; }
        public DbSet<PriceListName> PriceListName { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<BackingType> BackingTypes { get; set; }
        public DbSet<BackingPrice> BackingPrices { get; set; }
        public DbSet<BorderType> BorderTypes { get; set; }
    }
}