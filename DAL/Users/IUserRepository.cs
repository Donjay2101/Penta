using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAH.Models;
using DAH.ViewModel;

namespace DAH.DAL.Users
{
    interface IUserRepository:IRepository
    {
        IEnumerable<UserView> GetUsers();
        User UserByID(int ID);
        User UserByName(string Name);
        int GetUserID(string Name);
        IEnumerable<UserView> PendingUsers();
        //IEnumerable<UserView> PendingUsersId();
        bool Login(string Username,string password);
    }
}
