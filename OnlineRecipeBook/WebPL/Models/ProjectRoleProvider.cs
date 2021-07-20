using System;
using System.Web.Security;

namespace WebPL.Models
{
    public class ProjectRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            return (username == "admin" && roleName == "admin") ||
                   (username == "admin" && roleName == "user") ||
                   (!string.IsNullOrEmpty(username) && roleName == "user");

        }

        public override string[] GetRolesForUser(string username)
        {
            if (username == "admin")
                return new[] { "user", "admin" };
            else if (!string.IsNullOrEmpty(username))
                return new[] { "user" };
            else
                return new string[] { };
        }

        #region NotImplemented
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}