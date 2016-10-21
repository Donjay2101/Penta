using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAH.Models;
using DAH.ViewModel;

namespace DAH.DAL.Users
{
    public class UserRepository:IUserRepository
    {
        private DAHDbContext context;

        public UserRepository(DAHDbContext context)
        {
            this.context = context;
        }


        //All Users
        public IEnumerable<UserView> GetUsers()
        {
            var data = (from c in context.Users
                       join cd in context.CustomerCodes on c.ID equals cd.UserID
                           into cust
                       from rt in cust.DefaultIfEmpty()
                       select new UserView
                       {
                           Active = c.Active,
                           Address = c.Address,
                           AutoConfirmation = c.AutoConfirmation,
                           City = c.City,
                           Company = c.Company,
                           ContactName = c.ContactName,
                           CustomerCode = rt.Code,
                           CustomerInfoSent = c.CustomerInfoSent,
                           DepositConfirmation = c.DepositConfirmation,
                           DepositRequired = c.DepositRequired,
                           DropShipAddress = c.DropShipAddress,
                           DropShipCity = c.DropShipCity,
                           DropShipCompany = c.DropShipCompany,
                           DropShipState = c.DropShipState,
                           DropShipZip = c.DropShipZip,
                           Email = c.Email,
                           ID = c.ID,
                           ISDropShipAddress = c.ISDropShipAddress,
                           Keywords = c.Keywords,
                           Notes = c.Notes,
                           OtherPhone = c.OtherPhone,
                           Password = c.Password,
                           Phone = c.Phone,
                           PricingPage = c.PricingPage,
                           PricingPercent = c.PricingPercent,
                           ShipVia = c.ShipVia,
                           State = c.State,
                           TermsCode = c.TermsCode,
                           UserName = c.UserName,
                           Zip = c.Zip      
                       }).Where(x=>x.Active==true);
                     
          
            return data;
        }


        // Get User Id by Name
        public IEnumerable<UserView> PendingUsers()
        {
            var data = (from c in context.Users
                        join cd in context.CustomerCodes on c.ID equals cd.UserID
                            into cust
                        from rt in cust.DefaultIfEmpty()
                        select new UserView
                        {
                            Active = c.Active,
                            Address = c.Address,
                            AutoConfirmation = c.AutoConfirmation,
                            City = c.City,
                            Company = c.Company,
                            ContactName = c.ContactName,
                            CustomerCode = rt.Code,
                            CustomerInfoSent = c.CustomerInfoSent,
                            DepositConfirmation = c.DepositConfirmation,
                            DepositRequired = c.DepositRequired,
                            DropShipAddress = c.DropShipAddress,
                            DropShipCity = c.DropShipCity,
                            DropShipCompany = c.DropShipCompany,
                            DropShipState = c.DropShipState,
                            DropShipZip = c.DropShipZip,
                            Email = c.Email,
                            ID = c.ID,
                            ISDropShipAddress = c.ISDropShipAddress,
                            Keywords = c.Keywords,
                            Notes = c.Notes,
                            OtherPhone = c.OtherPhone,
                            Password = c.Password,
                            Phone = c.Phone,
                            PricingPage = c.PricingPage,
                            PricingPercent = c.PricingPercent,
                            ShipVia = c.ShipVia,
                            State = c.State,
                            TermsCode = c.TermsCode,
                            UserName = c.UserName,
                            Zip = c.Zip
                        }).Where(x => x.Active == false);

            return data;
        }


       


        // Get User By ID
        public User UserByID(int ID)
        {

            return context.Users.Where(x => x.ID == ID).FirstOrDefault();
        }



        // Get User By Name
        public User UserByName(string Name)
        {

            return context.Users.Where(x => x.UserName==Name).FirstOrDefault();
        }

        // Get User Id by Name
        public int GetUserID(string Name)
        {
            return context.Users.Where(x => x.UserName == Name).FirstOrDefault().ID;
        }

        public bool Login(string UserName,string Password)
        {
            var user = context.Users.Where(x => x.UserName == UserName && x.Password == Password && x.Active==true).FirstOrDefault();
            if(user!=null)
            {
                return true;
            }
            return false;
        }


        public void Insert(object ObjUser)
        {
            User user = ObjUser as User;
            if(user!=null)
            {
                context.Users.Add(user);
            }
            
        }

        public void Update(object user)
        {
            User us = user as User;
            if(us!=null)
            {
                context.Entry(user).State = EntityState.Modified;
            }
            
        }

        public void Delete(int ID)
        {
            User user = context.Users.Find(ID);
            context.Users.Remove(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}