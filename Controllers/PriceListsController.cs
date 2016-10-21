using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAH.DAL.PriceLists;
using DAH.Models;
using Newtonsoft.Json;

namespace DAH.Controllers
{
    public class PriceListsController : Controller
    {
        IPriceListRepository context = null;
        // GET: PriceLists

        public PriceListsController()
        {
            context = new PriceListRepository(new DAHDbContext());
        }
        public ActionResult Index()
        {

            if (Session["User"] == null)
            {
                return Redirect("/Account/Login");
            }

            return View();
        }

        public ActionResult PriceLists()
        {
            
            if(Session["User"]!=null)
            {
                User user = Session["User"] as User;
                var data = context.GetPriceLists(user.ID).ToList();

                return PartialView("_PriceLists", data);
            }

            return Redirect("/Account/login");
            

        }

        public ActionResult Edit(int ID)
        {
            var data = context.GetPriceListByID(ID);
            if(data==null)
            {
                return HttpNotFound();
            }
            ViewBag.Emblem = new SelectList(ShrdMaster.Instance.GetEmblemByPriceListID(data.PriceListNameID), "ID", "Value",data.Emblem);
           
            ViewBag.PriceListID = new SelectList(context.GePriceListNames().ToList(), "ID", "Name", data.PriceListNameID);
            
            

            return View(data);
        }

        [HttpPost]
        public ActionResult  SavePriceListName(int ID,string Name)
        {
            int result=0;
            if(!string.IsNullOrEmpty(Name))
            {
               
                if(!ShrdMaster.Instance.CheckPriceListName(Name))
                {
                    if (ID > 0)
                    {
                        context.EditPriceListName(ID, Name);
                        result = 1;
                    }
                    else
                    {
                        result = context.SavePriceListName(Name);
                    }
                    
                }
                else
                {
                    result = -1;
                }
                
            }

            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        
        [HttpPost]
        public ActionResult SaveEmblems(string model)
        {
            List<Emblem> data = JsonConvert.DeserializeObject<List<Emblem>>(model);
            if(data!=null)
            {
                context.SaveEmblems(data);
               
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("-1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PriceListNew(int ID)
        {
            ViewBag.Emblem=new SelectList(context.GetEmbelmsByPriceListID(ID),"ID","Value");

            return PartialView("_APriceListView");
        }

        [HttpPost]
        public ActionResult SavePriceList(string model)
        {
            List<PriceList> data = JsonConvert.DeserializeObject<List<PriceList>>(model);

            if(data!=null)
            {
                data.ForEach(x => { context.Insert(x); context.Save(); });
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            return Json("-1", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}