using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAH.DAL.CustomerCodes;
using DAH.DAL.Users;
using DAH.Models;
using DAH.ViewModel;
using Newtonsoft.Json;

namespace DAH.Controllers
{
    [Authorize(Roles="Admin")]
    public class UserController : Controller
    {
         IUserRepository userRepository;
         ICustomerCodes codecontext;
         DAHDbContext db = new DAHDbContext();


        public UserController()
        {
            userRepository=new UserRepository(new DAHDbContext());
            codecontext = new CustomerCodeRepository(new DAHDbContext());
        }

        // GET: User
        public ActionResult Index()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult UpdateCustomerCode(string Code)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var data=ser.Deserialize<List<CustomerCode>>(Code);
           
            foreach(CustomerCode code in data)
            {
                codecontext.Insert(code);
                codecontext.Save();
                var user = userRepository.UserByID(code.UserID);
                user.Active = true;
                userRepository.Update(user);
                userRepository.Save();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AssignCustomerCode()
        {
            //var data = userRepository.PendingUsers();
            ViewBag.UserID = new SelectList(userRepository.PendingUsers().ToList(), "ID", "ContactName");
            ViewBag.PriceList = new SelectList(db.PriceListName, "ID", "Name");
            return PartialView("_AssignCustomerCode");
        }


        public ActionResult User(int option=0)
        {
            List<UserView> users = null;
            if(option==0)
            {
                ViewBag.PendingUsers = true;
                users = userRepository.PendingUsers().ToList();
            }
            else
            {
                ViewBag.PendingUsers = false;
                users = userRepository.GetUsers().ToList();
                
            }
            
            return PartialView("_PendingUsers", users);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult GetPendingUsers()
        {
            var data=userRepository.PendingUsers();

         
            return PartialView("_PendingUsers",data);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(User model)
        {
            if(ModelState.IsValid)
            {

                var existeduser=userRepository.UserByName(model.UserName);
                if(existeduser!=null)
                {
                    ViewBag.Error = "Username already exists.Use another one.";
                    return View(model);
                }
                userRepository.Insert(model);
                userRepository.Save();
                int userID = userRepository.GetUserID(model.UserName);
                UserRole roles = new UserRole();
                roles.UserID = userID;
                roles.RoleID = 2;
                db.UserRoles.Add(roles);
                db.SaveChanges();
                return Redirect("/Account/Login");
            }

            return View(model);
        }


        public ActionResult Edit(int ID)
        {
            User user=userRepository.UserByID(ID);

            if(user!=null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var existeduser = userRepository.UserByID(model.ID);

                if(model.ID!= existeduser.ID && model.UserName==existeduser.UserName)
                {
                    ViewBag.Error = "Username already exists.Use another one."; 
                    return View(model);
                }
                model.Active = true;
                userRepository.Update(model);
                userRepository.Save();                
            }
            return Redirect("/User/Index");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            userRepository.Delete(ID);
            userRepository.Save();
            return Json("done",JsonRequestBehavior.AllowGet);
        }

    }
}