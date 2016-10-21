using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAH.Models;

namespace DAH.DAL.NPIEmblemOrders
{
    public class EmblemOrderRepository:IEmblemOrders
    {
         private DAHDbContext context;
         private bool disposed = false;

         public EmblemOrderRepository(DAHDbContext context)
        {
            this.context = context;
        }


         //All Users
         public IEnumerable<NPIEmblemOrder> GetOrders()
         {
             return context.Orders.AsEnumerable();
         }


         // Get User By ID
         public NPIEmblemOrder GetOrder(int ID)
         {

             return context.Orders.Find(ID);
         }

         public void Insert(object Objorder)
         {
             NPIEmblemOrder order = Objorder as NPIEmblemOrder;
             if(order!=null)
             {
                 context.Orders.Add(order);
             }
             
         }

         public void Update(object Objorder)
         {
             NPIEmblemOrder order = Objorder as NPIEmblemOrder;
             if(order!=null)
             {
                 context.Entry(order).State = EntityState.Modified;
             }
             
         }

         public void Delete(int ID)
         {
             NPIEmblemOrder user = context.Orders.Find(ID);
             context.Orders.Remove(user);
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