using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAH.Models;

namespace DAH.DAL.NPIEmblemOrders
{
    interface IEmblemOrders:IRepository
    {
        IEnumerable<NPIEmblemOrder> GetOrders();
        NPIEmblemOrder GetOrder(int ID);
    }
}
