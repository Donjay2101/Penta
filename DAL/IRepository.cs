using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAH.DAL
{
    interface IRepository:IDisposable
    {

        void Insert(object obj);
        void Delete(int obj);
        void Update(object obj);
        void Save();
    }
}
