using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAH.Models;
using DAH.ViewModel;
using System.Data.SqlClient;

namespace DAH.DAL.PriceLists
{
    public class PriceListRepository:IPriceListRepository
    {
         private DAHDbContext context;
         private bool disposed = false;

         public PriceListRepository(DAHDbContext context)
        {
            this.context = context;
        }


         //All Users
         public IEnumerable<PriceListViewModel> GetPriceLists(int ID)
         {
            
                return context.Database.SqlQuery<PriceListViewModel>("exec SP_GetPriceLists @id", new SqlParameter("@id", ID)).AsEnumerable();
            
             
         }


         // Get User By ID
         public PriceList GetPriceListByID(int ID)
         {

             return context.PriceLists.Find(ID);
         }


        public int SavePriceListName(string Name)
         {
            PriceListName p=new PriceListName ();
            p.Name=Name;
            context.PriceListName.Add(p);
            context.SaveChanges();
            return p.ID;
         }

        public void EditPriceListName(int ID,string Name)
        {
            PriceListName pricelist=context.PriceListName.Find(ID);
            if(pricelist!=null)
            {
                pricelist.Name = Name;
            }
            context.Entry(pricelist).State = EntityState.Modified;
            context.SaveChanges();

        }


         public IEnumerable<EmblemName> GetEmbelmsByPriceListID(int ID)
         {
            return ShrdMaster.Instance.GetEmblemByPriceListID(ID);// context.Emblems.Where(x => x.PriceList == ID).AsEnumerable();
         }


         public int SaveEmblems(List<Emblem> list)
         {
             list.ForEach(x => { context.Emblems.Add(x); context.SaveChanges(); });
             return 1;
         }

         public IEnumerable<PriceListName> GePriceListNames()
         {
             return context.PriceListName.AsEnumerable(); 
         }

         public void Insert(object Objorder)
         {
             PriceList order = Objorder as PriceList;
             if(order!=null)
             {
                 context.PriceLists.Add(order);
             }
             
         }




         public void Update(object Objorder)
         {
             PriceList order = Objorder as PriceList;
             if(order!=null)
             {
                 context.Entry(order).State = EntityState.Modified;
             }
             
         }

         public void Delete(int ID)
         {
             PriceList user = context.PriceLists.Find(ID);
             context.PriceLists.Remove(user);
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