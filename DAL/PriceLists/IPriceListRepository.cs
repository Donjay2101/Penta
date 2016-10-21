using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAH.Models;
using DAH.ViewModel;

namespace DAH.DAL.PriceLists
{
    interface IPriceListRepository:IRepository
    {
        IEnumerable<PriceListViewModel> GetPriceLists(int ID=0);
        PriceList GetPriceListByID(int ID);
        IEnumerable<EmblemName> GetEmbelmsByPriceListID(int ID);
        IEnumerable<PriceListName> GePriceListNames();
        int SavePriceListName(string Name);
        void EditPriceListName(int ID, string Name);
        int SaveEmblems(List<Emblem> list);



    }
}
