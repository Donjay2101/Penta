using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAH.Models
{
    public class ShrdMaster
    {


        static ShrdMaster _instance;

        DAHDbContext db = new DAHDbContext();  

        public static ShrdMaster Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new ShrdMaster();
                }

                return _instance;
            }
        }






        public bool ISAdmin(string username)
        {
            var data = db.Users.Where(x => x.UserName == username).FirstOrDefault();
            if(data!=null)
            {
                var roles = db.UserRoles.Where(x => x.UserID == data.ID && x.RoleID == 1).FirstOrDefault();
                if(roles!=null)
                {
                    return true;
                }                
            }
            return false;
        }

        
        public User LoggedInUser(string username)
        {

            var data = db.Users.Where(x => x.UserName == username).FirstOrDefault();
           return data;            
        }

        public bool CheckPriceListName(string Name)
        {
            var data=db.PriceListName.Where(x => x.Name == Name).FirstOrDefault();

            if(data!=null)
            {
                return true;
            }
            return false;
        }

        public List<ShipVia> GetShips()
        {
            List<ShipVia> ships = new List<ShipVia>() { 
            new ShipVia(){ID=1,Name="RED"},
            new ShipVia(){ID=1,Name="BLUE"},
            new ShipVia(){ID=1,Name="ORANGE"},
            new ShipVia(){ID=1,Name="GROUND"},
            new ShipVia(){ID=1,Name="OTHER"}
            };

            return ships;
        }

        public List<Sample> GetSamples()
        {
            List<Sample> samples = new List<Sample>() { 
              new Sample(){ID=1,Name="5-PRE PRO"},
              new Sample(){ID=1,Name="PAPER"},
              new Sample(){ID=1,Name="NO SAMPLE"}              
            };

            return samples;
        }

        public List<OrderedBy> GetOrders()
        {
            List<OrderedBy> orderby = new List<OrderedBy>()
            {
                new OrderedBy(){ID=1,Name="VERNON"},
                new OrderedBy(){ID=2,Name="STERLING"},
            };
            return orderby;
        }

        public List<PercentOfEmblem> GetPercentOfEmblems()
        {
            List<PercentOfEmblem> percent = new List<PercentOfEmblem>()
            {
                new PercentOfEmblem(){ID=1,Name="50%"},
                    new PercentOfEmblem(){ID=2,Name="70%"},
                    new PercentOfEmblem(){ID=3,Name="85%"},
                    new PercentOfEmblem(){ID=4,Name="100%"}
            };
            return percent;
        }

        public List<TypePricePercent> GetTypePricePercent()
        {
            List<TypePricePercent> price = new List<TypePricePercent>()
            {
                new TypePricePercent(){ID=1,Name="TWILL"},
                    new TypePricePercent(){ID=2,Name="FELT"},
                        new TypePricePercent(){ID=3,Name="NEON"},
                            new TypePricePercent(){ID=4,Name="NUER SUEDUE"},
            };
            return price;
        }

        public List<TypePrice> GetTypePrice()
        {
            List<TypePrice> price = new List<TypePrice>()
            {
                new TypePrice(){ID=1,Name="MERROW"},
                    new TypePrice(){ID=2,Name="LASER"},
                        new TypePrice(){ID=3,Name="CUSTOM CUT"},
                            new TypePrice(){ID=4,Name="BUTTON LOOP"},
                            new TypePrice(){ID=4,Name="STANDARD CUT"},
            };
            return price;
        }

        public List<Backing> GetBacking()
        {
            List<Backing> price = new List<Backing>()
            {
                new Backing(){ID=1,Name="HEAT SEAL"},
                    new Backing(){ID=2,Name="PLASTIC"},
                        new Backing(){ID=3,Name="ADHESIVE"},
                            new Backing(){ID=4,Name="PLAIN"},
                            new Backing(){ID=4,Name="VELCRO"},
            };
            return price;
        }

        public  CustomerCode getPriceListByUsername(string userName)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == userName);
            if(user!=null)
            {
                var customerCode = db.CustomerCodes.FirstOrDefault(x => x.UserID == user.ID);
                return customerCode;
            }
            return null;
        }

        public List<Shape> GetShape()
        {
            List<Shape> price = new List<Shape>()
            {
                new Shape(){ID=1,Name="CIRCLE"},
                    new Shape(){ID=2,Name="SQUARE"},
                        new Shape(){ID=3,Name="RECTANGLE"},
                            new Shape(){ID=4,Name="OTHER"}
                            
            };
            return price;
        }


        public List<SpecialThread> GetSpecialThread()
        {
            List<SpecialThread> price = new List<SpecialThread>()
            {
                new SpecialThread(){ID=1,Name="CIRCLE"},
                    new SpecialThread(){ID=2,Name="SQUARE"},
                        new SpecialThread(){ID=3,Name="RECTANGLE"},
                            new SpecialThread(){ID=4,Name="OTHER"}
                            
            };
            return price;
        }

        public List<EmblemName> GetEmblemByPriceListID(int ID)
        {
            return db.Database.SqlQuery<EmblemName>("sp_GetEmblemByPriceListID @PriceListID", new SqlParameter("@PriceListID", ID)).ToList();
        }


    }

    public class EmblemName
    {
       public int ID { get; set; }
        public string Name { get; set; }
    }
}