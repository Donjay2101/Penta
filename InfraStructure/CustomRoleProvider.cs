using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DAH.Models;

namespace DAH.InfraStructure
{
    public class CustomRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            using (var DB = new DAHDbContext())
            {
                var user = DB.Users.FirstOrDefault(u => u.UserName== username);
                if (user == null)
                    return false;
                var roles = DB.UserRoles.Where(x => x.UserID== user.ID);
                var roleInfo = DB.Roles.Where(x => x.RoleName == roleName);

                return roles != null && roleInfo != null;
            }
        }

        public override string[] GetRolesForUser(string username)                
       
        {
            using (var DAHDbContext = new DAHDbContext())
            {

                var user = DAHDbContext.Users.FirstOrDefault(u => u.UserName == username);

                if (user == null)
                {

                   
                        return new string[] { };               
                }
                else
                {
                    var roles = DAHDbContext.UserRoles.Where(x => x.UserID== user.ID).Select(x => x.RoleID).ToArray();
                    var selectedrole = (from role in DAHDbContext.Roles where roles.Contains(role.ID) select role);
                    var roletoUser = selectedrole.Select(x => x.RoleName).ToArray();
                    return roles == null ? new string[] { } : roletoUser;
                }

                //var rolesarray=allroles.ToArray();


                //return new string[] { };

            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            using (var DAHDbContext = new DAHDbContext())
            {
                return DAHDbContext.Roles.Select(r => r.RoleName).ToArray();
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}