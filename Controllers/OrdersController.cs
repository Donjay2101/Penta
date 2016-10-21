using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAH.DAL.CustomerCodes;
using DAH.DAL.NPIEmblemOrders;
using DAH.DAL.Users;
using DAH.Models;

namespace DAH.Controllers
{
    [Authorize(Roles="Admin,User")]
    public class OrdersController : Controller
    {
        IEmblemOrders orders;
        ICustomerCodes CustomerCodes;
        DAHDbContext db = new DAHDbContext();
        public OrdersController()
        {
            orders = new EmblemOrderRepository(new DAHDbContext());
            CustomerCodes = new CustomerCodeRepository(new DAHDbContext());
        }
        // GET: Orders
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Orders()
        {
            string username = User.Identity.Name;
            List<NPIEmblemOrder> orderlist=new List<NPIEmblemOrder> ();
            CustomerCode customercode;
            DAH.Models.User user=ShrdMaster.Instance.LoggedInUser(username);
            
            if(!ShrdMaster.Instance.ISAdmin(username))
            {
                if(user!=null)
                {
                     customercode=CustomerCodes.GetCustomerCodeByID(user.ID);
                    if(customercode!=null)
                    {
                        orderlist= orders.GetOrders().Where(x=>x.CustomerCode==customercode.ID) .ToList();
                    }
                    else
                    {
                        ViewBag.Error = "Customer Code not defined for user.";
                    }
                    
                }

                
            }
            else
            {
                orderlist = orders.GetOrders().ToList();
            }

            
            
            return PartialView("_Orders", orderlist);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            SetViewbag();
            int PONumber = 0;
            int.TryParse(db.Orders.Max(x => x.PONumber), out PONumber);
            ViewBag.PONumber = PONumber+1;
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(NPIEmblemOrder collection)
        {
            SetViewbag();
            try
            {
                // TODO: Add insert logic here
                //if(ModelState.IsValid)
                //{
                    orders.Insert(collection);
                    orders.Save();
                //}

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            SetViewbag();
            var data = orders.GetOrder(id);
            if(data==null)
            {
                return HttpNotFound();
            }
         

            return View(data);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(NPIEmblemOrder data)
        {
            orders.Update(data);
            orders.Save();
            SetViewbag();
            return RedirectToAction("Index");
        }

     
        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            orders.Delete(id);
            orders.Save();

            return Json("done", JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult GetMaterialTypeValue(int ID)
        {
            var data = db.MaterialTypes.Find(ID);
            if(data!=null)
            {
                return Json(data.Value, JsonRequestBehavior.AllowGet);
            }
            return Json("-1", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetBaseData(int quntity,double size=0,int emblem=0,int customerCode=0)
        {
            string username = User.Identity.Name;

            User user=ShrdMaster.Instance.LoggedInUser(username);


            var code= db.CustomerCodes.FirstOrDefault(x=>x.UserID==customerCode); 

            if(code != null)
            {
                double value = 0;
                var data = db.PriceLists.FirstOrDefault(x => x.Size == size && x.Emblem == emblem && x.PriceListNameID == code.PriceList);
                //var data = data1.FirstOrDefault();
                if (data != null)
                {
                    if (quntity <= 99)
                        value = data.First;
                    else if (quntity >= 100 && quntity <= 199)
                        value = data.Second;
                    else if (quntity >= 200 && quntity <= 299)
                        value = data.Third;
                    else if (quntity > 300 && quntity <= 499)
                        value = data.Fourth;
                    else if (quntity > 500 && quntity <= 999)
                        value = data.Fifth;
                    else if (quntity > 1000 && quntity <= 1999)
                        value = data.Sixth;
                    else if (quntity > 2000 && quntity <= 2999)
                        value = data.Seventh;
                    else if (quntity > 3000 && quntity <= 4999)
                        value = data.Eighth;
                    else if (quntity > 5000 && quntity <= 9999)
                        value = data.Ninth;
                    else
                    {
                        value = data.Tenth;
                        if (value == 0)
                        {
                            if (data.PriceListNameID == 1)
                            {
                                value = data.Seventh;
                            }
                            else
                            {
                                value = data.Sixth;
                            }

                        }
                    }
                  
                }
                return Json(value, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
            
          
        }


        public ActionResult GetBackingPrice(float Size,int BackingType)
        {
            var data = db.BackingPrices.Where(x => x.BackingType == BackingType && x.Size>=Size).ToList();

            BackingPrice br = null;
            double Cost=0;
            if (data.Count > 0)
            {
                if (Size >= 2.0 && Size < 3.0)
                {
                    br=data.Where(x => x.Size == 2).FirstOrDefault();
                    if(br!=null)
                    {
                        Cost =br.Cost;
                    }

                    
                }
                else if (Size >= 3.0 && Size < 3.5)
                {
                    br = data.Where(x => x.Size == 3).FirstOrDefault();
                    if (br != null)
                    {
                        Cost = br.Cost;
                    }
                    
                }
                else if (Size >= 3.5 && Size < 4.0)
                {
                    br = data.Where(x => x.Size == 3.5).FirstOrDefault();
                    if (br != null)
                    {
                        Cost = br.Cost;
                    }
                }
                else if (Size >= 4.0 && Size < 4.5)
                {
                    br = data.Where(x => x.Size == 4.0).FirstOrDefault();
                    if (br != null)
                    {
                        Cost = br.Cost;
                    }
                    
                }
                else if (Size >= 4.5 && Size < 5.0)
                {
                    br = data.Where(x => x.Size == 4.5).FirstOrDefault();
                    if (br != null)
                    {
                        Cost = br.Cost;
                    }
                }
                else if (Size >= 5.0)
                {
                    br = data.Where(x => x.Size == 5.0).FirstOrDefault();
                    if (br != null)
                    {
                        Cost = br.Cost;
                    }
                }
            }


            return Json(Cost, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetBorderType(int BType)
        {
            var data=db.BorderTypes.Find(BType);
            double cost=0;
            if(data!=null)
            {
                cost=data.Cost;
            }

            return Json(cost,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmblemByCustomerID(int customerID)
        {
            var customerCode = db.CustomerCodes.FirstOrDefault(x => x.UserID == customerID);
            var emblem = ShrdMaster.Instance.GetEmblemByPriceListID(customerCode.PriceList);
            HttpCookie pricelist = new HttpCookie("PriceList");
            pricelist.Value = customerCode.PriceList.ToString();
            Response.Cookies.Add(pricelist);
            return Json(emblem, JsonRequestBehavior.AllowGet);

            

            //return Json("", JsonRequestBehavior.AllowGet);

        }
        public void SetViewbag()
        {

            ViewBag.CustomerCode = new SelectList(CustomerCodes.GetCustomerCodes(), "UserID", "Code");
            ViewBag.ShipVia = new SelectList(ShrdMaster.Instance.GetShips(), "ID", "Name");
            ViewBag.Sample = new SelectList(ShrdMaster.Instance.GetSamples(), "ID", "Name");
            ViewBag.OrderedBy = new SelectList(ShrdMaster.Instance.GetOrders(), "ID", "Name");
            ViewBag.TypePricePercent = new SelectList(db.MaterialTypes.ToList(), "ID", "Name");
            ViewBag.TypePrice = new SelectList(db.BorderTypes.ToList(), "ID", "Name");
            ViewBag.Backing = new SelectList(db.BackingTypes.ToList(), "ID", "Name");
            ViewBag.Shape = new SelectList(ShrdMaster.Instance.GetShape(), "ID", "Name");
            ViewBag.PercentOfEmblem = new SelectList(ShrdMaster.Instance.GetPercentOfEmblems(), "ID", "Name");
            ViewBag.SpecialThread = new SelectList(ShrdMaster.Instance.GetSpecialThread(), "ID", "Name");
        }

    }
}
