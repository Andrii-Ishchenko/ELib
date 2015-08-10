using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using ELib.BL.Services.Abstract;
using ELib.BL.DtoEntities;

namespace ELib.Common.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private string applicationName;
        private IPersonRoleService roleservice;
        public CustomRoleProvider(IPersonRoleService role)
        {
            roleservice = role;
        }

        public override string[] GetRolesForUser(string login)
        {
            string role = roleservice.ReadRoleForUser(login);
            return new[] { role };
        }

        public override void CreateRole(string roleName)
        {
            PersonRoleDto role = new PersonRoleDto { Name = roleName };
            roleservice.Insert(role);
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            return roleservice.IsUserInRole(login, roleName);
        }

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            PersonRoleDto role = roleservice.GetByName(roleName);

            if (role == null) return false;

            roleservice.Delete(role);
            return true;
        }

        public override string[] GetAllRoles()
        {
            return roleservice.GetAll().Select(rolename => rolename.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return roleservice.ReadUsersInRole(roleName).ToArray();
        }

        public override bool RoleExists(string roleName)
        {
            return roleservice.Exists(roleName);
        }




        public override void AddUsersToRoles(string[] logins, string[] roleNames)
        {

            if (roleNames != null && roleNames.Length > 0)
                roleservice.AddUsersToRole(logins, roleNames[0]);
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
    }
}
