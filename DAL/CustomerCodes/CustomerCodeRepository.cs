using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAH.Models;

namespace DAH.DAL.CustomerCodes
{
    public class CustomerCodeRepository:ICustomerCodes
    {
          private DAHDbContext context;
         private bool disposed = false;

         public CustomerCodeRepository(DAHDbContext context)
        {
            this.context = context;
        }


         //All Users
         public IEnumerable<CustomerCode> GetCustomerCodes()
         {
            return context.CustomerCodes.AsEnumerable();


         }
        public List<CustomerCode> GetCustomerCodesList()
        {
            return context.Database.SqlQuery<CustomerCode>("sp_GetCustomerWithCode").ToList();
            //context.CustomerCodes.AsEnumerable();
        }

        // Get User By ID
        public User GetByCustomerCode(string ID)
         {
             var data = context.CustomerCodes.Where(x => x.Code == ID).FirstOrDefault();
             User user = null;
             if(data!=null)
             {
                 user = context.Users.Where(x => x.ID == data.UserID).FirstOrDefault();
             }
             return user;
         }

        public CustomerCode GetCustomerCodeByID(int ID)
        {
            var data = context.CustomerCodes.Where(x => x.UserID == ID).FirstOrDefault();
            if(data!=null)
            {
                return data;
            }

            return null;
        }

         public void Insert(object Objorder)
         {
             CustomerCode order = Objorder as CustomerCode;
             if(order!=null)
             {
                var data=context.CustomerCodes.Where(x => x.UserID == order.UserID).ToList();
                if(data!=null && data.Count>0)
                {
                    context.CustomerCodes.Where(x => x.UserID == order.UserID).ToList().ForEach(x =>
                    {
                        context.CustomerCodes.Remove(x);
                        context.SaveChanges();
                    });
                }

                context.CustomerCodes.Add(order);
            }
             
         }

         public void Update(object Objorder)
         {
             CustomerCode order = Objorder as CustomerCode;
             if(order!=null)
             {
                 context.Entry(order).State = EntityState.Modified;
             }
             
         }

         public void Delete(int ID)
         {
             //CustomerCode user = context.Orders.Find(ID);
             //context.Orders.Remove(user);
         }

         public void Save()
         {
             context.SaveChanges();
         }
                                
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