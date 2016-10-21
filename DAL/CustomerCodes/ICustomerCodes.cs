using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAH.Models;

namespace DAH.DAL.CustomerCodes
{
    interface ICustomerCodes:IRepository
    {

        IEnumerable<CustomerCode> GetCustomerCodes();
        List<CustomerCode> GetCustomerCodesList();

        User GetByCustomerCode(string ID);
        CustomerCode GetCustomerCodeByID(int ID);

    }
}
